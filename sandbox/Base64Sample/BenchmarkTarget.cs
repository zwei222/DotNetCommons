using System;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using DotNetCommons.Utilities;

namespace Base64Sample
{
    [Config(typeof(BenchmarkConfig))]
    public class BenchmarkTarget
    {
        private string _encodeSource;
        
        private byte[] _encodeBytesSource;
        
        private string _encodeUrlSource;

        private string _decodeSource;

        private string _decodeUrlSource;

        private Encoding _encoding;
        
        [GlobalSetup]
        public void Setup()
        {
            this._encoding = Encoding.UTF8;
            this._encodeSource = @"In computer science, Base64 is a group of binary-to-text encoding schemes that represent binary data in an ASCII string format by translating it into a radix-64 representation. The term Base64 originates from a specific MIME content transfer encoding. Each Base64 digit represents exactly 6 bits of data. Three 8-bit bytes (i.e., a total of 24 bits) can therefore be represented by four 6-bit Base64 digits.
Common to all binary-to-text encoding schemes, Base64 is designed to carry data stored in binary formats across channels that only reliably support text content. Base64 is particularly prevalent on the World Wide Web[1] where its uses include the ability to embed image files or other binary assets inside textual assets such as HTML and CSS files.[2]";
            this._encodeBytesSource = this._encoding.GetBytes(this._encodeSource);
            this._encodeUrlSource = @"[
  {
    ""_id"": ""5e377a33a295fb505cc0db70"",
    ""index"": 0,
    ""guid"": ""8ff7b26b-cbdd-4da4-9efa-3cd3c01b60cd"",
    ""isActive"": true,
    ""balance"": ""$3,066.93"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 38,
    ""eyeColor"": ""green"",
    ""name"": {
      ""first"": ""Melva"",
      ""last"": ""Moreno""
    },
    ""company"": ""SUREMAX"",
    ""email"": ""melva.moreno@suremax.biz"",
    ""phone"": ""+1 (981) 595-3324"",
    ""address"": ""371 Maujer Street, Mayfair, Minnesota, 307"",
    ""about"": ""Dolore irure do anim commodo id amet. Qui esse nulla ipsum ipsum nulla. Sunt et ullamco enim laborum cupidatat reprehenderit laboris adipisicing dolor Lorem amet magna magna labore. Nostrud culpa officia ut aute dolore tempor magna et veniam cillum. Nisi elit ut Lorem magna id laboris ad eiusmod irure dolor. Nulla commodo pariatur nisi cupidatat sint amet sit enim cillum veniam dolore sit laboris."",
    ""registered"": ""Tuesday, September 22, 2015 2:13 PM"",
    ""latitude"": ""-52.172153"",
    ""longitude"": ""144.303702"",
    ""tags"": [
      ""sunt"",
      ""amet"",
      ""in"",
      ""veniam"",
      ""aliquip""
    ],
    ""range"": [
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Marcella Kim""
      },
      {
        ""id"": 1,
        ""name"": ""Hanson Mcintosh""
      },
      {
        ""id"": 2,
        ""name"": ""Patton Barker""
      }
    ],
    ""greeting"": ""Hello, Melva! You have 10 unread messages."",
    ""favoriteFruit"": ""apple""
  },
  {
    ""_id"": ""5e377a33d164f7699ee3f8d7"",
    ""index"": 1,
    ""guid"": ""9ab0934d-61c8-40b2-a820-c2955d05830c"",
    ""isActive"": false,
    ""balance"": ""$1,361.21"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 23,
    ""eyeColor"": ""green"",
    ""name"": {
      ""first"": ""Gamble"",
      ""last"": ""Small""
    },
    ""company"": ""COASH"",
    ""email"": ""gamble.small@coash.us"",
    ""phone"": ""+1 (808) 475-2207"",
    ""address"": ""126 Duffield Street, Abiquiu, Colorado, 1499"",
    ""about"": ""Do adipisicing minim do nostrud dolor quis id velit eiusmod dolor eu non anim fugiat. Culpa tempor aliquip consequat est exercitation incididunt amet ea. Dolor aliqua aliqua nulla cupidatat officia do qui minim aute consectetur."",
    ""registered"": ""Thursday, May 15, 2014 7:00 PM"",
    ""latitude"": ""31.639226"",
    ""longitude"": ""-129.582777"",
    ""tags"": [
      ""consectetur"",
      ""ipsum"",
      ""ea"",
      ""nulla"",
      ""reprehenderit""
    ],
    ""range"": [
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Lula Faulkner""
      },
      {
        ""id"": 1,
        ""name"": ""Marion Hardy""
      },
      {
        ""id"": 2,
        ""name"": ""Corine Patel""
      }
    ],
    ""greeting"": ""Hello, Gamble! You have 8 unread messages."",
    ""favoriteFruit"": ""strawberry""
  },
  {
    ""_id"": ""5e377a333d8dbb1fac873b17"",
    ""index"": 2,
    ""guid"": ""d70b7427-1c82-4c1f-9635-e88431a10851"",
    ""isActive"": true,
    ""balance"": ""$2,943.79"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 25,
    ""eyeColor"": ""green"",
    ""name"": {
      ""first"": ""Sheryl"",
      ""last"": ""Russell""
    },
    ""company"": ""QUILM"",
    ""email"": ""sheryl.russell@quilm.io"",
    ""phone"": ""+1 (802) 589-3598"",
    ""address"": ""402 Wallabout Street, Cliff, New Hampshire, 6231"",
    ""about"": ""Occaecat magna cillum nulla irure nulla fugiat qui tempor mollit consectetur fugiat magna adipisicing. Ad reprehenderit reprehenderit incididunt laboris fugiat consectetur amet ut aute adipisicing. Ea nostrud ea eu culpa. In sunt elit do minim reprehenderit tempor cillum laborum magna minim. Aliqua ad dolore culpa cillum laboris ex culpa ut veniam cupidatat sint."",
    ""registered"": ""Tuesday, December 18, 2018 4:43 PM"",
    ""latitude"": ""-63.858587"",
    ""longitude"": ""-18.43647"",
    ""tags"": [
      ""in"",
      ""magna"",
      ""laboris"",
      ""est"",
      ""proident""
    ],
    ""range"": [
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Fleming Bush""
      },
      {
        ""id"": 1,
        ""name"": ""Chaney Whitley""
      },
      {
        ""id"": 2,
        ""name"": ""Anderson Rose""
      }
    ],
    ""greeting"": ""Hello, Sheryl! You have 9 unread messages."",
    ""favoriteFruit"": ""strawberry""
  },
  {
    ""_id"": ""5e377a33ae30b91746fb7fb3"",
    ""index"": 3,
    ""guid"": ""fdfc8371-fda2-461c-a0c8-f59fa1bd0407"",
    ""isActive"": true,
    ""balance"": ""$2,325.68"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 35,
    ""eyeColor"": ""brown"",
    ""name"": {
      ""first"": ""Christi"",
      ""last"": ""French""
    },
    ""company"": ""WEBIOTIC"",
    ""email"": ""christi.french@webiotic.co.uk"",
    ""phone"": ""+1 (807) 496-2839"",
    ""address"": ""307 Thames Street, Riner, New Mexico, 5446"",
    ""about"": ""Nulla pariatur excepteur veniam sit incididunt excepteur laborum culpa laborum incididunt irure aute. Id labore excepteur enim consectetur sint tempor sint duis do mollit aliquip minim amet. Culpa Lorem proident irure labore nostrud enim irure excepteur. Minim quis Lorem laborum et aute. Minim quis et elit irure veniam. Aliqua adipisicing excepteur duis sunt dolor Lorem adipisicing aliqua ea magna velit consectetur non pariatur. Nulla amet ea ullamco aute elit aliqua qui eu anim cillum."",
    ""registered"": ""Saturday, June 4, 2016 2:55 PM"",
    ""latitude"": ""-21.351173"",
    ""longitude"": ""-51.536985"",
    ""tags"": [
      ""sint"",
      ""in"",
      ""fugiat"",
      ""nisi"",
      ""non""
    ],
    ""range"": [
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Goodwin Rosales""
      },
      {
        ""id"": 1,
        ""name"": ""Kerr Meyers""
      },
      {
        ""id"": 2,
        ""name"": ""Alana Weber""
      }
    ],
    ""greeting"": ""Hello, Christi! You have 10 unread messages."",
    ""favoriteFruit"": ""strawberry""
  },
  {
    ""_id"": ""5e377a33034a3df480f8bd0d"",
    ""index"": 4,
    ""guid"": ""6e0e7b98-caeb-4f00-b081-a60ad01024be"",
    ""isActive"": true,
    ""balance"": ""$2,948.35"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 33,
    ""eyeColor"": ""green"",
    ""name"": {
      ""first"": ""Everett"",
      ""last"": ""Wilkinson""
    },
    ""company"": ""IPLAX"",
    ""email"": ""everett.wilkinson@iplax.com"",
    ""phone"": ""+1 (932) 487-3828"",
    ""address"": ""439 Moore Street, Austinburg, Florida, 3741"",
    ""about"": ""Consequat tempor do incididunt sint eu elit ut minim cupidatat nulla magna amet ipsum. Deserunt culpa incididunt eu culpa. Proident cillum sit esse est eu tempor proident qui pariatur mollit laboris commodo. Cupidatat fugiat sit in deserunt esse nostrud laboris Lorem occaecat consectetur."",
    ""registered"": ""Wednesday, April 16, 2014 5:42 PM"",
    ""latitude"": ""-85.028669"",
    ""longitude"": ""-92.235515"",
    ""tags"": [
      ""sint"",
      ""aliquip"",
      ""minim"",
      ""proident"",
      ""proident""
    ],
    ""range"": [
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Simpson Hoffman""
      },
      {
        ""id"": 1,
        ""name"": ""Todd Meadows""
      },
      {
        ""id"": 2,
        ""name"": ""Stevens Hurst""
      }
    ],
    ""greeting"": ""Hello, Everett! You have 8 unread messages."",
    ""favoriteFruit"": ""strawberry""
  },
  {
    ""_id"": ""5e377a33f1dfe0bb06b4bfc0"",
    ""index"": 5,
    ""guid"": ""78c011e8-fc77-46b4-8934-a81cf5df65b4"",
    ""isActive"": true,
    ""balance"": ""$3,240.21"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 32,
    ""eyeColor"": ""green"",
    ""name"": {
      ""first"": ""Atkinson"",
      ""last"": ""Fuentes""
    },
    ""company"": ""KONNECT"",
    ""email"": ""atkinson.fuentes@konnect.info"",
    ""phone"": ""+1 (800) 600-2232"",
    ""address"": ""528 Henry Street, Snowville, West Virginia, 8394"",
    ""about"": ""Proident aute ut ut tempor elit est anim anim minim ipsum exercitation ad quis cillum. Magna ex ad culpa magna proident ad. Laboris excepteur proident dolore ea eu excepteur anim id. Cupidatat adipisicing esse aliquip ad incididunt eiusmod enim sit. Consectetur pariatur occaecat non excepteur fugiat officia aute sunt reprehenderit culpa anim in dolor. Elit excepteur excepteur magna nulla id ex ea enim aliquip mollit minim enim."",
    ""registered"": ""Wednesday, January 8, 2020 5:12 PM"",
    ""latitude"": ""-63.705738"",
    ""longitude"": ""64.470027"",
    ""tags"": [
      ""ex"",
      ""eiusmod"",
      ""in"",
      ""eiusmod"",
      ""do""
    ],
    ""range"": [
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Mavis Cleveland""
      },
      {
        ""id"": 1,
        ""name"": ""Malinda Hines""
      },
      {
        ""id"": 2,
        ""name"": ""Hart Carver""
      }
    ],
    ""greeting"": ""Hello, Atkinson! You have 7 unread messages."",
    ""favoriteFruit"": ""banana""
  },
  {
    ""_id"": ""5e377a33d43031ec484f2581"",
    ""index"": 6,
    ""guid"": ""05c1498e-ff3c-4ea0-9538-df1c95b2dee1"",
    ""isActive"": false,
    ""balance"": ""$1,349.00"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 26,
    ""eyeColor"": ""brown"",
    ""name"": {
      ""first"": ""Hays"",
      ""last"": ""Dennis""
    },
    ""company"": ""UNCORP"",
    ""email"": ""hays.dennis@uncorp.tv"",
    ""phone"": ""+1 (949) 538-3269"",
    ""address"": ""502 Florence Avenue, Sattley, District Of Columbia, 9169"",
    ""about"": ""Et dolore exercitation fugiat amet adipisicing quis irure do officia et ipsum officia nostrud sunt. Quis laborum qui fugiat quis deserunt excepteur sit. Reprehenderit Lorem excepteur labore ullamco ad aute voluptate veniam duis do."",
    ""registered"": ""Sunday, October 9, 2016 11:15 AM"",
    ""latitude"": ""75.512066"",
    ""longitude"": ""102.045663"",
    ""tags"": [
      ""qui"",
      ""non"",
      ""mollit"",
      ""excepteur"",
      ""velit""
    ],
    ""range"": [
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Vinson Berry""
      },
      {
        ""id"": 1,
        ""name"": ""Fern Hobbs""
      },
      {
        ""id"": 2,
        ""name"": ""Hickman Gilbert""
      }
    ],
    ""greeting"": ""Hello, Hays! You have 9 unread messages."",
    ""favoriteFruit"": ""banana""
  },
  {
    ""_id"": ""5e377a33e6b78a1ab3bdd43e"",
    ""index"": 7,
    ""guid"": ""fba6d284-1c61-4bb2-9fb3-f22daf343575"",
    ""isActive"": true,
    ""balance"": ""$3,037.66"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 38,
    ""eyeColor"": ""brown"",
    ""name"": {
      ""first"": ""Savage"",
      ""last"": ""Howell""
    },
    ""company"": ""ZIPAK"",
    ""email"": ""savage.howell@zipak.ca"",
    ""phone"": ""+1 (925) 413-3240"",
    ""address"": ""869 Forbell Street, Bladensburg, Virgin Islands, 5026"",
    ""about"": ""Excepteur labore non velit Lorem proident ad incididunt ad voluptate esse pariatur aliquip adipisicing tempor. Elit excepteur nulla qui dolore magna duis. Ut id ullamco et laborum nostrud adipisicing. Ea cillum ipsum esse duis labore eu sit labore commodo culpa. Ad amet commodo sit culpa. Nostrud adipisicing mollit cillum ut."",
    ""registered"": ""Monday, December 2, 2019 2:54 PM"",
    ""latitude"": ""-21.17137"",
    ""longitude"": ""-101.381421"",
    ""tags"": [
      ""quis"",
      ""ipsum"",
      ""eu"",
      ""nulla"",
      ""ipsum""
    ],
    ""range"": [
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Marquita Roth""
      },
      {
        ""id"": 1,
        ""name"": ""Gwen Waller""
      },
      {
        ""id"": 2,
        ""name"": ""Larsen Wagner""
      }
    ],
    ""greeting"": ""Hello, Savage! You have 8 unread messages."",
    ""favoriteFruit"": ""banana""
  },
  {
    ""_id"": ""5e377a33972bcf99a09a7a16"",
    ""index"": 8,
    ""guid"": ""7bc7451c-4095-4f16-a94b-1ead3688e790"",
    ""isActive"": true,
    ""balance"": ""$1,443.55"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 24,
    ""eyeColor"": ""blue"",
    ""name"": {
      ""first"": ""Bobbie"",
      ""last"": ""Pratt""
    },
    ""company"": ""ECRATIC"",
    ""email"": ""bobbie.pratt@ecratic.name"",
    ""phone"": ""+1 (993) 421-3657"",
    ""address"": ""193 Monument Walk, Goldfield, Iowa, 1186"",
    ""about"": ""Cupidatat consectetur culpa ea ut veniam esse nisi id magna ullamco voluptate veniam ut. Esse aliqua cupidatat occaecat excepteur aliquip magna et duis eiusmod esse irure ex. Eu irure enim sit minim esse excepteur est officia labore velit magna. Ea nostrud sit amet in. Velit cillum magna voluptate mollit fugiat dolore anim. Consectetur adipisicing magna laborum do officia in."",
    ""registered"": ""Wednesday, March 30, 2016 8:46 AM"",
    ""latitude"": ""-72.168799"",
    ""longitude"": ""-20.933615"",
    ""tags"": [
      ""eu"",
      ""non"",
      ""amet"",
      ""officia"",
      ""non""
    ],
    ""range"": [
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Lupe Lloyd""
      },
      {
        ""id"": 1,
        ""name"": ""Milagros Robles""
      },
      {
        ""id"": 2,
        ""name"": ""Beatriz Curry""
      }
    ],
    ""greeting"": ""Hello, Bobbie! You have 6 unread messages."",
    ""favoriteFruit"": ""banana""
  }
]";
            this._decodeSource = Convert.ToBase64String(_encoding.GetBytes(this._encodeSource));
            this._decodeUrlSource = Convert.ToBase64String(this._encoding.GetBytes(this._encodeSource)).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            Base64.Encode(nameof(this._encodeSource), this._encoding);
        }

        [Benchmark(Description = "Base64.Encode(string)")]
        public string Encode()
        {
            return Base64.Encode(this._encodeSource, this._encoding);
        }

        [Benchmark(Description = "Convert.ToBase64String(string)")]
        public string EncodeConvert()
        {
            return Convert.ToBase64String(this._encoding.GetBytes(this._encodeSource));
        }

        [Benchmark(Description = "Base64.Encode(byte[])")]
        public string EncodeBytes()
        {
            return Base64.Encode(this._encodeBytesSource);
        }

        [Benchmark(Description = "Convert.ToBase64String(byte[])")]
        public string EncodeBytesConvert()
        {
            return Convert.ToBase64String(this._encodeBytesSource);
        }

        [Benchmark(Description = "Base64.EncodeUrl(string)")]
        public string EncodeUrl()
        {
          return Base64.EncodeUrl(this._encodeUrlSource, this._encoding);
        }

        [Benchmark(Description = "Convert.ToBase64String(string).TrimEnd('=').Replace('+', '-').Replace('/', '_')")]
        public string EncodeConvertUrl()
        {
          return Convert.ToBase64String(this._encoding.GetBytes(this._encodeUrlSource)).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        [Benchmark(Description = "Base64.Decode(string)")]
        public string Decode()
        {
            return Base64.Decode(this._decodeSource, this._encoding);
        }

        [Benchmark(Description = "Convert.FromBase64String(string)")]
        public string DecodeConvert()
        {
            return this._encoding.GetString(Convert.FromBase64String(this._decodeSource));
        }

        [Benchmark(Description = "Base64.DecodeUrl(string)")]
        public string DecodeUrl()
        {
          return Base64.Decode(this._decodeUrlSource, this._encoding);
        }

        [Benchmark(Description = "Convert.FromBase64String(StringBuilder.ToString())")]
        public string DecodeConvertUrl()
        {
          var stringBuilder = new StringBuilder(this._decodeUrlSource);

          stringBuilder.Append(Enumerable.Range(0, this._decodeUrlSource.Length % 4).Select(_ => '=').ToArray().AsSpan())
            .Replace('-', '+').Replace('_', '/');
          return this._encoding.GetString(Convert.FromBase64String(stringBuilder.ToString()));
        }
    }
}