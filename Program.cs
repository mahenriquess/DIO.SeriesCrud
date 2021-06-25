using System;

namespace SeriesCrud
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
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
						throw new ArgumentOutOfRangeException();
				}

				userOption = GetUserOption();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
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
			foreach (int i in Enum.GetValues(typeof(Gender)))
			{
				Console.WriteLine($"{i}-{Enum.GetName(typeof(Gender), i)}");
			}
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

			repositorio.Insert(newSerie);
		}
        private static void UpdateSerie()
		{
			Console.Write("Digite o id da série: ");
			int indexSerie = int.Parse(Console.ReadLine());

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
            foreach (int i in Enum.GetValues(typeof(Gender)))
			{
				Console.WriteLine($"{i}-{Enum.GetName(typeof(Gender), i)}");
			}
			Console.Write("Selecione um dos generos acima: ");
			int genderInput = int.Parse(Console.ReadLine());

			Console.Write("Insira o Título: ");
			string titleInput = Console.ReadLine();

			Console.Write("Insira o Ano de Lançamento: ");
			int yearInput = int.Parse(Console.ReadLine());

			Console.Write("Descrição: ");
			string descriptionInput = Console.ReadLine();

            Serie updateSerie = new Serie(
                id          : indexSerie,
                gender      : (Gender)genderInput,
				title       : titleInput,
				year        : yearInput,
				description : descriptionInput 
            );

			repositorio.Update(indexSerie, updateSerie);
		}
        private static void ShowSerie()
		{
			Console.Write("Digite o id da série: ");
			int indexSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.ReturnForId(indexSerie);

			Console.WriteLine(serie);
		}
        private static void DeleteSerie()
		{
			Console.Write("Digite o id da série: ");
			int indexSerie = int.Parse(Console.ReadLine());

			repositorio.Delete(indexSerie);
		}

        private static string GetUserOption()
		{
			Console.WriteLine();
			Console.WriteLine("Bem vindo ao meu gerenciador de series!");
			Console.WriteLine("Selecione uma opção abaixo para continuar:");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string userOption = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return userOption;
		}
    }
}
