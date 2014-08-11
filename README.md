===========================
C# DataRepository to MySQL!
===========================

An pretty simple, and easy form to use ADO .NET C#
No Dll's, Lib's, or anything else!
Just download the class files, put in your App_Code directory and enjoy!

**How to use!**

  - First Of All!
    - **1.1** Installing

  - Type of Methods!
    - **2.1** Modifiers
    - **2.2** Retrieves
    
  - Accompaniments!
    - **3.1** BusinessRepository&lt;T&gt;

------------  
First Of All!
------------
**1.1 - Installing**

*You just have to download the archive class: data_repository.cs, and if you want the business_repository.cs to more explanation scroll down at the 3.1 section!*


------------  
Type of Methods!
------------
**2.1 - Modifiers**

*Methods that will be modify rows, (INSERT, UPDATE, DELETE)*

```
public bool Add()
{
  try
  {
    string sql = @"INSERT INTO tags (total_posts, name, uri) 
                   VALUES (@total_posts, @name, @uri);";
    return DataRepository.ChangeRecords(GetParameters(), sql);
  }
  catch (Exception ex)
  {
    throw new Exception(ex.Message);
  }
}
```

Instead of:


```
public bool Add()
{
  bool isOk = false;
  try
  {
    using (MySqlConnection conn = dbConnect.SiteConnection())
    {
      string sql = @"INSERT INTO tags (total_posts, name, uri) 
                     VALUES (@total_posts, @name, @uri);";
      using(MySqlCommand cmd = new MySqlCommand(sql, conn))
      {
        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = Name;
        cmd.Parameters.Add("@uri", MySqlDbType.VarChar).Value = Uri;
        cmd.Parameters.Add("@total_posts", MySqlDbType.Int32).Value = TotalPosts;
        conn.Open();
        isOk = cmd.ExecuteNonQuery() > 0 ? true : false;
      }
    }
  }
  catch (Exception ex)
  {
    throw ex;
  }
  return isOk;
}
```
