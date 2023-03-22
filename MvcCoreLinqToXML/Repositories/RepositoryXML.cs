using MvcCoreLinqToXML.Helpers;
using MvcCoreLinqToXML.Models;
using System.Xml.Linq;

namespace MvcCoreLinqToXML.Repositories
{
    public class RepositoryXML
    {
        private HelperPathProvider helper;

        public RepositoryXML(HelperPathProvider helper)
        {
            this.helper = helper;
        }

        public List<Joyeria> GetJoyerias()
        {
            string path = helper.MapPath("joyerias.xml"
                , Folders.Documents);
            //AQUI TENEMOS DOS METODOS PARA CARGAR DATOS EN UN 
            //OBJETO XDocument
            //1) Parse(string XML)
            //2) Load(path XML)
            XDocument document = XDocument.Load(path);
            //DEBEMOS EXTRAER LOS DATOS MANUALMENTE
            List<Joyeria> joyerias = new List<Joyeria>();
            var consulta = from datos in document.Descendants("joyeria")
                           select datos;
            //RECORREMOS TODOS LOS OBJETOS XELEMENT
            foreach (XElement tag in consulta)
            {
                Joyeria joyeria = new Joyeria();
                //PARA ACCEDER AL VALOR DE UN TAG SE UTILIZA Element
                //PARA ACCEDER AL VALOR DE UN ATRIBUTO SE UTILIZA Attribute
                joyeria.Nombre = tag.Element("nombrejoyeria").Value;
                joyeria.CIF = tag.Attribute("cif").Value;
                joyeria.Telefono = tag.Element("telf").Value;
                joyeria.Direccion = tag.Element("direccion").Value;
                joyerias.Add(joyeria);
            }
            return joyerias;
        }
    }
}
