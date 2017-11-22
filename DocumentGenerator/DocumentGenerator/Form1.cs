using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentGenerator
{
    public partial class DocumentGenerator : Form
    {
        List<Web> webs;
        public DocumentGenerator()
        {
            InitializeComponent();
        }

        private void SubirFichero(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "TXT Files (*.txt)|*.txt",
                FilterIndex = 0,
                DefaultExt = "txt"
            };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                
                    if ((myStream = openDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            var lines = ReadLines(myStream).ToList();
                            var webs = lines.Select(x=> {
                                var campos = x.Split('\t');
                                var nombre = campos[1];
                                List<double> normalizadas;
                                string firma = getFirmaFromCampos(campos,out normalizadas);
                                return new Web(nombre, firma,normalizadas);
                            });
                            this.webs = webs.ToList();
                        }
                    }
                
               
            }
        }

        private string getFirmaFromCampos(string[] campos,out List<double> normalizadas)
        {
            double max = 0;
            for (int i = 2; i < campos.Length; i++)
            {
                if (Double.Parse(campos[i]) > max) max = Double.Parse(campos[i]);
            }
            normalizadas = new List<double>();
            for (int i = 2; i < campos.Length; i++)
            {
                normalizadas.Add(Double.Parse(campos[i]) / max * 100);
            }
            List<double> t_t3 = new List<double>();
            for (int i = 5; i < normalizadas.Count; i++)
            {
                t_t3.Add(normalizadas[i]-normalizadas[i - 5]);
            }
            string firma = "";
            t_t3.ForEach(x =>
            {
                if (x <= -60) firma += "E";
                else if (x <= -20) firma += "D";
                else if (x <= 0) firma += "S";
                else if (x <= 60) firma += "U";
                else firma += "A";
            });
            return firma;
        }

        private void Guardar(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                Title = "Guarda el archivo xml generado"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string ruta = saveDialog.FileName;

                if (File.Exists(ruta))
                    File.Delete(ruta);

                using (var fileStream = File.Create(ruta))
                {
                    foreach (var item in this.webs)
                    {
                        var texto = new UTF8Encoding(true).GetBytes(item.ToString()+"\n");
                        fileStream.Write(texto, 0, texto.Length);
                        fileStream.Flush();
                    }
                }
            }
            saveDialog.Dispose();
            saveDialog = null;
        }

        public IEnumerable<string> ReadLines(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        private void Indexar(object sender, EventArgs e)
        {
            var docs = webs.Select(x => 
                new {nombre = x.Nombre, firma = x.generarFirma(), values = x.Values}
            );
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"));

            // use NEST *ElasticClient*
            var client = new ElasticClient(settings);

            var bulkResponse = client.Bulk(b => b
                .IndexMany(docs, (d, web) => d.Document(web).Index("webs-index").Type("web"))
            );
            this.Close();
        }

    }

}

