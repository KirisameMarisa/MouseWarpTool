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
                string path = Directory.GetCurrentDirectory() + "\\" + xml;
                //!< ReadOnlyを削除
                EraceFileAttribute(path, FileAttributes.ReadOnly);
                using (var fs = new FileStream(path, FileMode.Open))
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
            string path = Directory.GetCurrentDirectory() + "\\" + xml;
            //!< ReadOnlyを削除
            EraceFileAttribute(path, FileAttributes.ReadOnly);
            //!< ファイルExport
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                // XmlSerializerを使ってファイルに保存（TwitSettingオブジェクトの内容を書き込む）
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                // オブジェクトをシリアル化してXMLファイルに書き込む
                serializer.Serialize(fs, GetConfig());
            }
        }

        private void EraceFileAttribute(string path, FileAttributes attrbite)
        {          
            // ファイル属性を取得
            FileAttributes fas = File.GetAttributes(path);

            // 読み取り専用かどうか確認
            if ((fas & attrbite) == attrbite)
            {
                // ファイル属性から読み取り専用を削除
                fas = fas & ~attrbite;
            }

            // ファイル属性を設定
            File.SetAttributes(path, fas);
        }
    }
}
