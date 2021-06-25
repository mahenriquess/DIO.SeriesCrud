using System;

namespace SeriesCrud
{
    public class Serie : EntidadeBase
    {
        private Gender Gender { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private int Year { get; set; }
        private bool Deleted { get; set; }

        public Serie(int id, Gender gender, string title, string description, int year){
            this.Id = id;
            this.Gender = gender;
            this.Title = title;
            this.Description = description;
            this.Year = year;
            this.Deleted = false;
        }
            
        public override string ToString()
		{
			// Environment.NewLine https://docs.microsoft.com/en-us/dotnet/api/system.environment.newline?view=netcore-3.1
            string retorno = "";
            retorno += "Gênero: " + this.Gender + Environment.NewLine;
            retorno += "Titulo: " + this.Title + Environment.NewLine;
            retorno += "Descrição: " + this.Description + Environment.NewLine;
            retorno += "Ano de Início: " + this.Year + Environment.NewLine;
            retorno += "Excluido: " + this.Deleted; 
			return retorno;
		}
        
        public string returnTitle(){
            return this.Title;
        }
        public int returnId(){
            return this.Id;
        }
        public bool returnDeleted(){
            return this.Deleted;
        }
        public void Delete()
        {
            this.Deleted = true;
        }
    }
}