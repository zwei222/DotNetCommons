// ---------------------------------------------------------------------
// <copyright file="Base64.cs" company="zwei222">
// Copyright (c) zwei222. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------

using System;
using System.Buffers;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DotNetCommons.Utilities
{
    /// <summary>
    /// Manipulate strings in base64 format.
    /// </summary>
    public static unsafe class Base64
    {
        /// <summary>
        /// Base64 encoding correspondence table.
        /// </summary>
        private static readonly char[] Base64EncodingTable =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/',
        };

        /// <summary>
        /// Base64 URL encoding correspondence table.
        /// </summary>
        private static readonly char[] Base64UrlEncodingTable =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_',
        };

        /// <summary>
        /// UTF-8 format Base64 encoding correspondence table.
        /// </summary>
        private static readonly byte[] Base64UTF8EncodingTable;

        /// <summary>
        /// UTF-8 format Base64 URL encoding correspondence table.
        /// </summary>
        private static readonly byte[] Base64UTF8UrlEncodingTable;

        /// <summary>
        /// Base64 decoding correspondence table.
        /// </summary>
        private static readonly sbyte[] Base64DecodingTable;

        /// <summary>
        /// Base64 URL decoding correspondence table.
        /// </summary>
        private static readonly sbyte[] Base64UrlDecodingTable;

        static Base64()
        {
            Base64UTF8EncodingTable = BuildUtf8EncodingTable(Base64EncodingTable);
            Base64UTF8UrlEncodingTable = BuildUtf8EncodingTable(Base64UrlEncodingTable);
            Base64DecodingTable = BuildDecodingTable(Base64EncodingTable);
            Base64UrlDecodingTable = BuildDecodingTable(Base64UrlEncodingTable);
        }

        /// <summary>
        /// Encodes a character string into BASE64 format.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="encoding">String encoding.</param>
        /// <returns>BASE64 format string.</returns>
        public static string Encode(string source, Encoding encoding = default)
        {
            var sourceBytes = encoding.GetBytes(source);

            return Encode(sourceBytes);
        }

        /// <summary>
        /// Encode the byte array to BASE64 format.
        /// </summary>
        /// <param name="source">Source byte array.</param>
        /// <returns>BASE64 format string.</returns>
        public static string Encode(byte[] source)
        {
            var buffers = ArrayPool<char>.Shared.Rent(GetEncodedLength(source.Length));

            try
            {
                var bufferSpan = buffers.AsSpan();

                TryEncode(source, bufferSpan, out var result);

                return new string(bufferSpan.Slice(0, result));
            }
            finally
            {
                ArrayPool<char>.Shared.Return(buffers);
            }
        }

        /// <summary>
        /// Decodes a character string from BASE64 format.
        /// </summary>
        /// <param name="source">BASE64 string.</param>
        /// <param name="encoding">String encoding.</param>
        /// <returns>Source string.</returns>
        public static string Decode(string source, Encoding encoding)
        {
            return Decode(source.AsSpan(), encoding);
        }

        /// <summary>
        /// Decode the byte array from BASE64 format.
        /// </summary>
        /// <param name="sourceSpan">BASE64 char Span.</param>
        /// <param name="encoding">String encoding.</param>
        /// <returns>Source string.</returns>
        public static string Decode(ReadOnlySpan<char> sourceSpan, Encoding encoding)
        {
            var buffers = ArrayPool<byte>.Shared.Rent(GetDecodedLength(sourceSpan.Length));

            try
            {
                var bufferSpan = buffers.AsSpan();

                TryDecode(sourceSpan, bufferSpan, out var result);

                return encoding.GetString(bufferSpan.Slice(0, result));
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffers);
            }
        }

        /// <summary>
        /// Encodes a character string into BASE64 URL format.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="encoding">String encoding.</param>
        /// <returns>BASE64 URL format string.</returns>
        public static string EncodeUrl(string source, Encoding encoding = default)
        {
            var sourceBytes = encoding.GetBytes(source);

            return EncodeUrl(sourceBytes);
        }

        /// <summary>
        /// Encode the byte array to BASE64 URL format.
        /// </summary>
        /// <param name="source">Source byte array.</param>
        /// <returns>BASE64 URL format string.</returns>
        public static string EncodeUrl(byte[] source)
        {
            var buffers = ArrayPool<char>.Shared.Rent(GetEncodedUrlLength(source.Length));

            try
            {
                var bufferSpan = buffers.AsSpan();

                TryEncodeUrl(source, bufferSpan, out var result);

                return new string(bufferSpan.Slice(0, result));
            }
            finally
            {
                ArrayPool<char>.Shared.Return(buffers);
            }
        }

        /// <summary>
        /// Decodes a character string from BASE64 URL format.
        /// </summary>
        /// <param name="source">BASE64 URL string.</param>
        /// <param name="encoding">String encoding.</param>
        /// <returns>Source string.</returns>
        public static string DecodeUrl(string source, Encoding encoding)
        {
            return DecodeUrl(source.AsSpan(), encoding);
        }

        /// <summary>
        /// Decode the byte array from BASE64 URL format.
        /// </summary>
        /// <param name="sourceSpan">BASE64 URL char Span.</param>
        /// <param name="encoding">String encoding.</param>
        /// <returns>Source string.</returns>
        public static string DecodeUrl(ReadOnlySpan<char> sourceSpan, Encoding encoding)
        {
            var buffers = ArrayPool<byte>.Shared.Rent(GetDecodedUrlLength(sourceSpan.Length));

            try
            {
                var bufferSpan = buffers.AsSpan();

                TryDecodeUrl(sourceSpan, bufferSpan, out var result);

                return encoding.GetString(bufferSpan.Slice(0, result));
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffers);
            }
        }

        /// <summary>
        /// Encode the contents of Span to BASE64 format.
        /// </summary>
        /// <param name="byteSpan">Byte array of character strings.</param>
        /// <param name="charSpan">Encoding buffer.</param>
        /// <param name="result">Character string length after encoding.</param>
        /// <returns>A boolean value indicating whether encoding is complete.</returns>
        public static bool TryEncode(ReadOnlySpan<byte> byteSpan, Span<char> charSpan, out int result)
        {
            fixed (byte* input = &MemoryMarshal.GetReference(byteSpan))
            fixed (char* output = &MemoryMarshal.GetReference(charSpan))
            {
                result = EncodeBase64(input, output, 0, byteSpan.Length, Base64EncodingTable, true);

                return true;
            }
        }

        /// <summary>
        /// Decode the contents of Span from BASE64 format.
        /// </summary>
        /// <param name="charSpan">Encoding buffer.</param>
        /// <param name="byteSpan">Byte array of character strings.</param>
        /// <param name="result">Character string length after encoding.</param>
        /// <returns>A boolean value indicating whether encoding is complete.</returns>
        public static bool TryDecode(ReadOnlySpan<char> charSpan, Span<byte> byteSpan, out int result)
        {
            fixed (char* input = &MemoryMarshal.GetReference(charSpan))
            fixed (byte* output = &MemoryMarshal.GetReference(byteSpan))
            {
                return DecodeBase64(input, output, 0, charSpan.Length, Base64DecodingTable, true, out result);
            }
        }

        /// <summary>
        /// Encode the contents of Span to BASE64 URL format.
        /// </summary>
        /// <param name="byteSpan">Byte array of character strings.</param>
        /// <param name="charSpan">Encoding buffer.</param>
        /// <param name="result">Character string length after encoding.</param>
        /// <returns>A boolean value indicating whether encoding is complete.</returns>
        public static bool TryEncodeUrl(ReadOnlySpan<byte> byteSpan, Span<char> charSpan, out int result)
        {
            fixed (byte* input = &MemoryMarshal.GetReference(byteSpan))
            fixed (char* output = &MemoryMarshal.GetReference(charSpan))
            {
                result = EncodeBase64(input, output, 0, byteSpan.Length, Base64UrlEncodingTable, false);

                return true;
            }
        }

        /// <summary>
        /// Decode the contents of Span from BASE64 URL format.
        /// </summary>
        /// <param name="charSpan">Encoding buffer.</param>
        /// <param name="byteSpan">Byte array of character strings.</param>
        /// <param name="result">Character string length after encoding.</param>
        /// <returns>A boolean value indicating whether encoding is complete.</returns>
        public static bool TryDecodeUrl(ReadOnlySpan<char> charSpan, Span<byte> byteSpan, out int result)
        {
            fixed (char* input = &MemoryMarshal.GetReference(charSpan))
            fixed (byte* output = &MemoryMarshal.GetReference(byteSpan))
            {
                return DecodeBase64(input, output, 0, charSpan.Length, Base64UrlDecodingTable, false, out result);
            }
        }

        private static int EncodeBase64(byte* bytes, char* chars, int offset, int length, char[] table, bool padding)
        {
            var modulo = length % 3;
            var loopLength = offset + (length - modulo);
            var index = 0;
            var charIndex = 0;

            fixed (char* encodingTable = &table[0])
            {
                for (index = offset; index < loopLength; index += 3)
                {
                    chars[charIndex] = encodingTable[(bytes[index] & 0b11111100) >> 2];
                    chars[charIndex + 1] =
                        encodingTable[(bytes[index] & 0b00000011) << 4 | (bytes[index + 1] & 0b11110000) >> 4];
                    chars[charIndex + 2] =
                        encodingTable[(bytes[index + 1] & 0b00001111) << 2 | (bytes[index + 2] & 0b11000000) >> 6];
                    chars[charIndex + 3] = encodingTable[bytes[index + 2] & 0b00111111];
                    charIndex += 4;
                }

                index = loopLength;

                if (modulo == 2)
                {
                    chars[charIndex] = encodingTable[(bytes[index] & 0b11111100) >> 2];
                    chars[charIndex + 1] =
                        encodingTable[(bytes[index] & 0b00000011) << 4 | (bytes[index + 1] & 0b11110000) >> 4];
                    chars[charIndex + 2] = encodingTable[(bytes[index + 1] & 0b00001111) << 2];

                    if (padding)
                    {
                        chars[charIndex + 3] = '=';
                        charIndex += 4;
                    }
                    else
                    {
                        charIndex += 3;
                    }
                }
                else if (modulo == 1)
                {
                    chars[charIndex] = encodingTable[(bytes[index] & 0b11111100) >> 2];
                    chars[charIndex + 1] = encodingTable[(bytes[index] & 0b00000011) << 4];

                    if (padding)
                    {
                        chars[charIndex + 2] = '=';
                        chars[charIndex + 3] = '=';
                        charIndex += 4;
                    }
                    else
                    {
                        charIndex += 2;
                    }
                }

                return charIndex;
            }
        }

        private static bool DecodeBase64(
            char* chars,
            byte* bytes,
            int offset,
            int length,
            sbyte[] table,
            bool padding,
            out int result)
        {
            if (length == 0)
            {
                result = 0;

                return true;
            }

            var loopLength = offset + length - 4;
            var index = 0;
            var byteIndex = 0;

            fixed (sbyte* decodingTable = &table[0])
            {
                for (index = offset; index < loopLength;)
                {
                    ref var index0 = ref decodingTable[chars[index]];
                    ref var index1 = ref decodingTable[chars[index + 1]];
                    ref var index2 = ref decodingTable[chars[index + 2]];
                    ref var index3 = ref decodingTable[chars[index + 3]];

                    if (((index0 | index1 | index2 | index3) & 0b10000000) == 0b10000000)
                    {
                        result = 0;

                        return false;
                    }

                    var result0 = (byte)(((index0 & 0b00111111) << 2) | ((index1 & 0b00110000) >> 4));
                    var result1 = (byte)(((index1 & 0b00001111) << 4) | ((index2 & 0b00111100) >> 2));
                    var result2 = (byte)(((index2 & 0b00000011) << 6) | (index3 & 0b00111111));

                    bytes[byteIndex] = result0;
                    bytes[byteIndex + 1] = result1;
                    bytes[byteIndex + 2] = result2;
                    index += 4;
                    byteIndex += 3;
                }

                var restLength = length - index;

                if (padding)
                {
                    if (restLength != 4)
                    {
                        result = 0;

                        return false;
                    }

                    {
                        ref var index0 = ref decodingTable[chars[index]];
                        ref var index1 = ref decodingTable[chars[index + 1]];
                        ref var index2 = ref decodingTable[chars[index + 2]];
                        ref var index3 = ref decodingTable[chars[index + 3]];

                        if (index3 == -2)
                        {
                            if (index2 == -2)
                            {
                                if (index1 == -2)
                                {
                                    if (index0 == -2)
                                    {
                                        // No processing.
                                    }

                                    result = 0;

                                    return false;
                                }

                                {
                                    if (!IsValid(ref index0, ref index1))
                                    {
                                        result = 0;

                                        return false;
                                    }

                                    var result0 = (byte)((index0 & 0b00111111) << 2 | (index1 & 0b00110000) >> 4);

                                    bytes[byteIndex] = result0;
                                    byteIndex += 1;
                                    result = byteIndex;

                                    return true;
                                }
                            }

                            {
                                if (!IsValid(ref index0, ref index1, ref index2))
                                {
                                    result = 0;

                                    return false;
                                }

                                var result0 = (byte)((index0 & 0b00111111) << 2 | (index1 & 0b00110000) >> 4);
                                var result1 = (byte)((index1 & 0b00001111) << 4 | (index2 & 0b00111100) >> 2);

                                bytes[byteIndex] = result0;
                                bytes[byteIndex + 1] = result1;
                                byteIndex += 2;
                                result = byteIndex;

                                return true;
                            }
                        }
                        else
                        {
                            if (!IsValid(ref index0, ref index1, ref index2, ref index3))
                            {
                                result = 0;

                                return false;
                            }

                            var result0 = (byte)((index0 & 0b00111111) << 2 | (index1 & 0b00110000) >> 4);
                            var result1 = (byte)((index1 & 0b00001111) << 4 | (index2 & 0b00111100) >> 2);
                            var result2 = (byte)((index2 & 0b00000011) << 6 | (index3 & 0b00111111));

                            bytes[byteIndex] = result0;
                            bytes[byteIndex + 1] = result1;
                            bytes[byteIndex + 2] = result2;
                            byteIndex += 3;
                            result = byteIndex;

                            return true;
                        }
                    }
                }
                else
                {
                    if (restLength == 4)
                    {
                        ref var index0 = ref decodingTable[chars[index]];
                        ref var index1 = ref decodingTable[chars[index + 1]];
                        ref var index2 = ref decodingTable[chars[index + 2]];
                        ref var index3 = ref decodingTable[chars[index + 3]];

                        if (!IsValid(ref index0, ref index1, ref index2, ref index3))
                        {
                            result = 0;

                            return false;
                        }

                        var result0 = (byte)((index0 & 0b00111111) << 2 | (index1 & 0b00110000) >> 4);
                        var result1 = (byte)((index1 & 0b00001111) << 4 | (index2 & 0b00111100) >> 2);
                        var result2 = (byte)((index2 & 0b00000011) << 6 | (index3 & 0b00111111));

                        bytes[byteIndex] = result0;
                        bytes[byteIndex + 1] = result1;
                        bytes[byteIndex + 2] = result2;
                        byteIndex += 3;
                        result = byteIndex;

                        return true;
                    }
                    else if (restLength == 3)
                    {
                        ref var index0 = ref decodingTable[chars[index]];
                        ref var index1 = ref decodingTable[chars[index + 1]];
                        ref var index2 = ref decodingTable[chars[index + 2]];

                        if (!IsValid(ref index0, ref index1, ref index2))
                        {
                            result = 0;

                            return false;
                        }

                        var result0 = (byte)((index0 & 0b00111111) << 2 | (index1 & 0b00110000) >> 4);
                        var result1 = (byte)((index1 & 0b00001111) << 4 | (index2 & 0b00111100) >> 2);

                        bytes[byteIndex] = result0;
                        bytes[byteIndex + 1] = result1;
                        byteIndex += 2;
                        result = byteIndex;

                        return true;
                    }
                    else if (restLength == 2)
                    {
                        ref var index0 = ref decodingTable[chars[index]];
                        ref var index1 = ref decodingTable[chars[index + 1]];

                        if (!IsValid(ref index0, ref index1))
                        {
                            result = 0;

                            return false;
                        }

                        var result0 = (byte)((index0 & 0b00111111) << 2 | (index1 & 0b00110000) >> 4);

                        bytes[byteIndex] = result0;
                        byteIndex += 1;
                        result = byteIndex;

                        return true;
                    }
                    else
                    {
                        ref var index0 = ref decodingTable[chars[index]];

                        if (!IsValid(ref index0))
                        {
                            result = 0;

                            return false;
                        }

                        var result0 = (byte)((index0 & 0b00111111) << 2);

                        bytes[byteIndex] = result0;
                        byteIndex += 1;
                        result = byteIndex;

                        return true;
                    }
                }
            }
        }

        private static int GetEncodedLength(int length)
        {
            if (length == 0)
            {
                return 0;
            }

            var result = ((length + 2) / 3) * 4;

            return result == 0 ? 4 : result;
        }

        private static int GetDecodedLength(int length)
        {
            return (length / 4) * 3;
        }

        private static int GetDecodedUrlLength(int length)
        {
            if (length == 0)
            {
                return 0;
            }

            var modulo = length % 4;

            return ((length / 4) * 3) + modulo;
        }

        private static int GetEncodedUrlLength(int length)
        {
            if (length == 0)
            {
                return 0;
            }

            var modulo = length % 3;

            return ((length / 3) * 4) + (modulo == 0 ? 0 : modulo + 1);
        }

        private static bool IsValid(ref sbyte index0)
        {
            return (index0 & 0b10000000) != 0b10000000;
        }

        private static bool IsValid(ref sbyte index0, ref sbyte index1)
        {
            return ((index0 | index1) & 0b10000000) != 0b10000000;
        }

        private static bool IsValid(ref sbyte index0, ref sbyte index1, ref sbyte index2)
        {
            return ((index0 | index1 | index2) & 0b10000000) != 0b10000000;
        }

        private static bool IsValid(ref sbyte index0, ref sbyte index1, ref sbyte index2, ref sbyte index3)
        {
            return ((index0 | index1 | index2 | index3) & 0b10000000) != 0b10000000;
        }

        private static byte[] BuildUtf8EncodingTable(char[] encodingTable)
        {
            return encodingTable.Select(character => (byte)character).ToArray();
        }

        private static sbyte[] BuildDecodingTable(char[] encodingTable)
        {
            var tableDictionary = encodingTable.Select((character, index) => (character, index))
                .ToDictionary(pair => pair.character, pair => pair.index);
            var decodingTable = new sbyte[char.MaxValue];

            for (var index = 0; index < char.MaxValue; index++)
            {
                if (tableDictionary.TryGetValue((char)index, out var value))
                {
                    decodingTable[index] = (sbyte)value;
                }
                else
                {
                    if ((char)index == '=')
                    {
                        decodingTable[index] = -2;
                    }
                    else
                    {
                        decodingTable[index] = -1;
                    }
                }
            }

            return decodingTable;
        }
    }
}