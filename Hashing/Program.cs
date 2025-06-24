using System;
using System.Collections.Generic;

class HashTable
{
    // chave e valor
    class Entry
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public Entry(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"[{Key} : {Value}]";
        }
    }

    private List<Entry>[] buckets;
    private int size;
    private int count;

    public HashTable(int capacity)
    {
        size = capacity;
        count = 0;
        buckets = new List<Entry>[size];
        for (int i = 0; i < size; i++)
        {
            buckets[i] = new List<Entry>();
        }
    }

    // Função
    private int Hash(string key)
    {
        int hash = 0;
        foreach (char c in key)
        {
            hash += c;
        }
        return hash % size;
    }

    // Inserir ou atualizar
    public void Insert(string key, string value)
    {
        int index = Hash(key);
        foreach (Entry entry in buckets[index])
        {
            if (entry.Key == key)
            {
                entry.Value = value;
                Console.WriteLine($"Chave '{key}' atualizada com sucesso.");
                return;
            }
        }

        buckets[index].Add(new Entry(key, value));
        count++;
        Console.WriteLine($"Chave '{key}' inserida com sucesso.");
    }

    // Buscar por chave
    public string Get(string key)
    {
        int index = Hash(key);
        foreach (Entry entry in buckets[index])
        {
            if (entry.Key == key)
                return entry.Value;
        }
        return null;
    }

    // Remover
    public void Remove(string key)
    {
        int index = Hash(key);
        for (int i = 0; i < buckets[index].Count; i++)
        {
            if (buckets[index][i].Key == key)
            {
                buckets[index].RemoveAt(i);
                count--;
                Console.WriteLine($"Chave '{key}' removida com sucesso.");
                return;
            }
        }
        Console.WriteLine($"Chave '{key}' não encontrada.");
    }

    // Imprimir tabela
    public void PrintTable()
    {
        Console.WriteLine("\n--- Tabela Hash ---");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"Índice {i}: ");
            if (buckets[i].Count == 0)
            {
                Console.WriteLine("(vazio)");
            }
            else
            {
                foreach (var entry in buckets[i])
                    Console.Write($"{entry} ");
                Console.WriteLine();
            }
        }
        Console.WriteLine($"Total de elementos: {count}");
        Console.WriteLine($"Fator de carga: {(double)count / size:F2}");
        Console.WriteLine("--------------------\n");
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Informe o tamanho da tabela hash: ");
        int capacity = int.Parse(Console.ReadLine());
        var hashTable = new HashTable(capacity);

        while (true)
        {
            Console.WriteLine("Escolha uma operação: [1] Inserir [2] Buscar [3] Remover [4] Imprimir [0] Sair");
            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Chave: ");
                    string chave = Console.ReadLine();
                    Console.Write("Valor: ");
                    string valor = Console.ReadLine();
                    hashTable.Insert(chave, valor);
                    break;

                case "2":
                    Console.Write("Chave para buscar: ");
                    string buscar = Console.ReadLine();
                    var resultado = hashTable.Get(buscar);
                    if (resultado != null)
                        Console.WriteLine($"Valor encontrado: {resultado}");
                    else
                        Console.WriteLine("Chave não encontrada.");
                    break;

                case "3":
                    Console.Write("Chave para remover: ");
                    string remover = Console.ReadLine();
                    hashTable.Remove(remover);
                    break;

                case "4":
                    hashTable.PrintTable();
                    break;

                case "0":
                    Console.WriteLine("Encerrando...");
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
