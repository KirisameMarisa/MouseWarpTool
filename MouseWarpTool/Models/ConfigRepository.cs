using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace MouseWarpTool.Models
{
    class ConfigRepository
    {
        /// <summary>
        /// 保存先
        /// </summary>
        private Config _config = null;

        /// <summary>
        /// オブジェクトファイルをセーブ
        /// </summary>
        /// <param name="settings"></param>
        public void Save(Config settings)
        {
            _config = settings;
        }

        /// <summary>
        /// XMLの情報をセーブ
        /// </summary>
        /// <param name="xml"></param>
        public void Save( string xml )
        {
            Config config = new Config();
            try
            {
                using (var fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + xml, FileMode.Open))
                {
                    // XmlSerializerを使ってファイルに保存
                    XmlSerializer serializer = new XmlSerializer(typeof(Config));
                    config = (Config)serializer.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            Save(config);
        }

        /// <summary>
        /// コンフィグ取得
        /// </summary>
        /// <returns></returns>
        public Config GetConfig()
        {
            return _config;
        }

        /// <summary>
        /// XMLに書き出し
        /// </summary>
        public void ExportXML(string xml)
        {
            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + xml, FileMode.Create))
            {
                // XmlSerializerを使ってファイルに保存（TwitSettingオブジェクトの内容を書き込む）
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                // オブジェクトをシリアル化してXMLファイルに書き込む
                serializer.Serialize(fs, GetConfig());
            }
        }
    }
}
