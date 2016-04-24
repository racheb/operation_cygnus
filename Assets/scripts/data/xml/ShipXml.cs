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

        /// <summary>
        /// Loads the ship data from a text asset.
        /// </summary>
        /// <param name="xml">The xml file as text asset</param>
        /// <returns>The ship data</returns>
        public static ShipXml Load(TextAsset xml) {
            return Load(xml.text);
        }

        /// <summary>
        /// Loads the ship data from xml as a string.
        /// </summary>
        /// <param name="text">The xml file as string</param>
        /// <returns>The ship data</returns>
        public static ShipXml Load(string text) {
            var serializer = new XmlSerializer(typeof(ShipXml));
            return serializer.Deserialize(new StringReader(text)) as ShipXml;
        }

        /// <summary>
        /// Loads the ship data from a xml file inside unity's asset folder structure.
        /// </summary>
        /// <param name="path">Relative path to the xml file from the Assets folder</param>
        /// <returns>The ship data</returns>
        public static ShipXml LoadFile(string path) {
            var serializer = new XmlSerializer(typeof(ShipXml));
            string finalPath = Path.Combine(Application.dataPath, path);
            using (var stream = new FileStream(finalPath, FileMode.Open)) {
                return serializer.Deserialize(stream) as ShipXml;
            }
        }

    }

    public class ShipGridXml {

        [XmlArray("prefabs")]
        [XmlArrayItem("prefab")]
        public List<ShipPrefabXml> PrefabList = new List<ShipPrefabXml>();

        [XmlArray("rows")]
        [XmlArrayItem("row")]
        public List<ShipRowsXml> RowList = new List<ShipRowsXml>();

        private Dictionary<char, GameObject> dict; 

        /// <summary>
        /// Converts the raw tile data into an array of corresponding prefab objects
        /// </summary>
        /// <returns>Array of prefabs</returns>
        public GameObject[,] GetPrefabArray() {
            var width = RowList[0].Row.Length;
            var height = RowList.Count;
            var prefabs = new GameObject[width, height];

            for (var r = 0; r < height; r++) {
                var row = RowList[r].Row;
                for (var c = 0; c < width; c++) {
                    prefabs[c, height - 1 - r] = GetPrefabDict()[row[c]];
                }
            }
            return prefabs;
        }

        /// <summary>
        /// Returns the dictionary that maps characters in the xml file's grid definition to their
        /// respective prefabs.
        /// </summary>
        /// <returns>char to GameObject dictionary</returns>
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

    }

    public class ShipRowsXml {

        [XmlText]
        public string Row;

    }

}
