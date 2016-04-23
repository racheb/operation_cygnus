using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.scripts.data.xml {

    [XmlRoot("ship")]
    public class ShipXml {

        [XmlElement("grid")]
        public ShipGridXml Grid;

        public static ShipXml Load(TextAsset xml) {
            return Load(xml.text);
        }

        public static ShipXml Load(string text) {
            var serializer = new XmlSerializer(typeof(ShipXml));
            return serializer.Deserialize(new StringReader(text)) as ShipXml;
        }

        public static ShipXml LoadFile(string path) {
            var serializer = new XmlSerializer(typeof(ShipXml));
            string finalPath = Path.Combine(Application.dataPath, path);
            using (var stream = new FileStream(finalPath, FileMode.Open)) {
                return serializer.Deserialize(stream) as ShipXml;
            }
        }

        public override string ToString() {
            return "[ShipXml]\n" + Grid.ToString() + "[/ShipXml]\n";
        }
    }

    public class ShipGridXml {

        /*
        [XmlAttribute("width")]
        public int Width;

        [XmlAttribute("height")]
        public int Height;
        */

        [XmlArray("prefabs")]
        [XmlArrayItem("prefab")]
        public List<ShipPrefabXml> PrefabList = new List<ShipPrefabXml>();

        [XmlArray("rows")]
        [XmlArrayItem("row")]
        public List<ShipRowsXml> RowList = new List<ShipRowsXml>();

        private Dictionary<char, GameObject> dict; 

        public override string ToString() {
            int Width = RowList[0].Row.Length;
            int Height = RowList.Count;

            var str = "[ShipGridXml]\nWidth -> " + Width + "\nHeight -> " + Height
                   + "\n[PrefabList]\n";
            str = PrefabList.Aggregate(str, (current, prefabXml) => current + prefabXml.ToString());
            str += "[/PrefabList]\n[RowList]\n";
            str = RowList.Aggregate(str, (current, rowXml) => current + rowXml.ToString());
            str += "[/RowList]\n[/ShipGridXml]\n";
            return str;
        }

        public GameObject[,] GetPrefabArray() {
            int Width = RowList[0].Row.Length;
            int Height = RowList.Count;

            var prefabs = new GameObject[Width, Height];

            for (var r = 0; r < Height; r++) {
                var row = RowList[r].Row;
                for (var c = 0; c < Width; c++) {
                    prefabs[c, Height - 1 - r] = GetPrefabDict()[row[c]];
                }
            }

            return prefabs;
        }

        public Dictionary<char, GameObject> GetPrefabDict() {
            return dict ?? (dict = PrefabList.ToDictionary(prefab => prefab.Char[0], prefab => 
                ShipViewScene.Get().TilePrefabs[prefab.PrefabName]));
        }

    }

    public class ShipPrefabXml {

        [XmlAttribute("char")]
        public string Char;

        [XmlText]
        public string PrefabName;

        public override string ToString() {
            return "[ShipPrefabXml]\nChar -> " + Char + "\nPrefabName -> " + PrefabName + "\n[/ShipPrefabXml]\n";
        }
    }

    public class ShipRowsXml {

        [XmlText]
        public string Row;

        public override string ToString() {
            return "[ShipRowsXml]\nRow -> " + Row + "\n[/ShipRowXml]\n";
        }
    }

}
