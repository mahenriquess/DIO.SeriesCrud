using System;

namespace SeriesCrud
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            Console.Clear();
            string userOption = GetUserOption();

			while (userOption.ToUpper() != "X")
			{
				switch (userOption)
				{
					case "1":
						ListSeries();
						break;
					case "2":
						InsertSerie();
						break;
					case "3":
						UpdateSerie();
						break;
					case "4":
						DeleteSerie();
						break;
					case "5":
						ShowSerie();
						break;
					case "C":
						Console.Clear();
						break;
					default:
                        InvalidOption();
						break;
				}

				userOption = GetUserOption();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
        }
        private static void ListSeries()
		{
			Console.WriteLine("Listar séries");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}
			foreach (var serie in lista)
			{               
				Console.WriteLine($"#ID {serie.returnId()}: - {serie.returnTitle()} {(serie.returnDeleted() ? "*Excluído*" : "")}");
			}
		}
        private static void InsertSerie()
		{
			Console.WriteLine("Cadastrar nova série");

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			ListGender();
			Console.Write("Selecione um dos generos acima: ");
			int genderInput = int.Parse(Console.ReadLine());

			Console.Write("Insira o Título: ");
			string titleInput = Console.ReadLine();

			Console.Write("Insira o Ano de Lançamento: ");
			int yearInput = int.Parse(Console.ReadLine());

			Console.Write("Descrição: ");
			string descriptionInput = Console.ReadLine();

			Serie newSerie = new Serie(
                id          : repositorio.NextId(),
                gender      : (Gender)genderInput,
				title       : titleInput,
				year        : yearInput,
				description : descriptionInput 
            );

            if(ValidateData(newSerie))
            {
			    repositorio.Insert(newSerie);
            }
		}
        private static void UpdateSerie()
		{
            try
            {
			    Console.Write("Digite o id da série: ");
			    int indexSerie = int.Parse(Console.ReadLine());
            
                var serie = repositorio.ReturnForId(indexSerie);
                
                // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
                // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1

                ListGender();
                Console.WriteLine($"Selecione um dos generos acima: (pressione 'Enter' para manter como está: {serie.returnGender()})");
                string auxGenderInput = Console.ReadLine();
                int genderInput;
                if(auxGenderInput == "")
                {
                    genderInput = (int)serie.returnGender();
                } 
                else
                {
                    genderInput = int.Parse(auxGenderInput);
                }

                Console.WriteLine($"Insira o Título: (pressione 'Enter' para manter como está: {serie.returnTitle()}) ");
                string titleInput = Console.ReadLine();
                if(titleInput == "")
                {
                    titleInput = serie.returnTitle();
                }

                Console.WriteLine($"Insira o Ano de Lançamento: (pressione 'Enter' para manter como está: {serie.returnYear()}) ");
                string auxYearInput = Console.ReadLine();
                int yearInput;
                if(auxYearInput == "")
                {
                    yearInput = serie.returnYear();
                }
                else
                {
                    yearInput = int.Parse(auxYearInput);
                }

                Console.WriteLine($"Descrição: (pressione 'Enter' para manter como está: {serie.returnDescription()}) ");
                string descriptionInput = Console.ReadLine();
                if(descriptionInput == "")
                {
                    descriptionInput = serie.returnDescription();
                }

                Serie updateSerie = new Serie(
                    id          : indexSerie,
                    gender      : (Gender)genderInput,
                    title       : titleInput,
                    year        : yearInput,
                    description : descriptionInput 
                );

                if(ValidateData(updateSerie))
                {
                    repositorio.Update(indexSerie, updateSerie);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine($"Erro: {e.Message}");
                Console.WriteLine("Voltando ao menu anterior!");
                return;
            }
		}
        private static void ShowSerie()
		{
            try
            {	Console.Write("Digite o id da série: ");
                int indexSerie = int.Parse(Console.ReadLine());

                var serie = repositorio.ReturnForId(indexSerie);
                Console.WriteLine(serie);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine($"Erro: {e.Message}");
                Console.WriteLine("Voltando ao menu anterior!");
                return;
            }
		}
        private static void DeleteSerie()
		{   
            ListSeries();
            try
            {
                Console.Write("Digite o id da série que deseja excluir: ");
                int indexSerie = int.Parse(Console.ReadLine());
                var serie = repositorio.ReturnForId(indexSerie);

                Console.WriteLine($"Você escolheu a série: {serie.returnTitle()}!");
                Console.WriteLine("Tem certeza que deseja excluir? (S - Sim | N - Não)");
                var decision = Console.ReadLine();
                switch(decision.ToUpper())
                {
                    case "S":
                        repositorio.Delete(indexSerie);
                        Console.WriteLine("Serie excluída com sucesso!");
                        Console.WriteLine("Voltando ao menu anterior.");
                        break;
                    case "N":
                        Console.WriteLine("Operação cancelada!! Pressione qualquer tecla para continuar.");
                        break;
                    default:
                        InvalidOption();
                        break;
                }	
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine($"Erro: {e.Message}");
                Console.WriteLine("Voltando ao menu anterior!");
                return;
            }
		}
        private static string GetUserOption()
		{
			Console.WriteLine();
			Console.WriteLine("Bem vindo ao meu gerenciador de series!");
			Console.WriteLine("Selecione uma opção abaixo para continuar:");
			Console.WriteLine("1 - Listar séries");
			Console.WriteLine("2 - Inserir nova série");
			Console.WriteLine("3 - Atualizar série");
			Console.WriteLine("4 - Excluir série");
			Console.WriteLine("5 - Visualizar série");
			Console.WriteLine("C - Limpar Tela");
			Console.WriteLine("X - Sair");
			Console.WriteLine();

			string userOption = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return userOption;
		}
        private static void InvalidOption()
        {
            Console.WriteLine("Opção inválida, voltando ao menu anterior!");
        }
        private static void ListGender()
        {
            foreach (int i in Enum.GetValues(typeof(Gender)))
			{
				Console.WriteLine($"{i} - {Enum.GetName(typeof(Gender), i)}");
			}
        }
        private static bool ValidateData(Serie serie)
        {
            if( ((int)serie.returnGender()) > Enum.GetValues(typeof(Gender)).Length)
            {
                Console.WriteLine("Genero inválido! Retornando ao menu anterior!");
                return false;
            }
            DateTime now = DateTime.Now;
            if(serie.returnYear() > now.Year)
            {
                Console.WriteLine("Ano inválido! Retornando ao menu anterior!");
                return false;
            }
            if(serie.returnTitle() == "")
            {
                Console.WriteLine("Titulo inválido! Retornando ao menu anterior!");
                return false;
            }
            if(serie.returnDescription() == "")
            {
                Console.WriteLine("Descrição inválida! Retornando ao menu anterior!");
                return false;
            }
            return true;
        }
    }
}
