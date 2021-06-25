using System;
using System.Collections.Generic;
using SeriesCrud.interfaces;

namespace SeriesCrud
{
    public class SerieRepositorio : IRepositorio<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>();
        public List<Serie> Lista()
        {
            return listaSerie;
        }
        public Serie ReturnForId(int id)
        {
           return listaSerie[id];
        }
        public void Insert(Serie entidade)
        {
            listaSerie.Add(entidade);
        }
        public void Delete(int id)
        {
            listaSerie[id].Delete();
        }
        public void Update(int id, Serie serie)
        {
            listaSerie[id] = serie;
        }
        public int NextId()
        {
            return listaSerie.Count;
        }
    }
}