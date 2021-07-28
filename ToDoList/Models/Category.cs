using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Category
  {
    private static List<Category> _instances = new List<Category> { };
    public string Name { get; set; }
    public int Id { get; }
    public List<Item> Items { get; set; }

    public Category(string categoryName)
    {
      Name = categoryName;
      _instances.Add(this);
      Id = _instances.Count;
      Items = new List<Item> { };
    }
    public Category(string name, int id)
    {
      Name = name;
      Id = id;
    }

    public static void ClearAll()
    {
      _instances.Clear();
    }

    public static List<Category> GetAll()
    {
      List<Category> allCategories = new List<Category> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM categories;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int categoryId = rdr.GetInt32(0);
        string categoryDescription = rdr.GetString(1);
        Category newCategory = new Category(categoryDescription, categoryId);
        allCategories.Add(newCategory);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCategories;
    }

    public static Category Find(int searchId)
    {
      return _instances[searchId - 1];
    }

    public void AddItem(Item item)
    {
      Items.Add(item);
    }

  }
}