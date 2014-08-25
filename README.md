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
      - **2.2.1** Extension Method (Verify)
    
  - Accompaniments!
    - **3.1** BusinessRepository&lt;T&gt;

-------------
First Of All!
-------------
**1.1 - Installing**

*You just have to download the archive class: data_repository.cs, and if you want the business_repository.cs to more explanation scroll down at the 3.1 section!*


----------------
Type of Methods!
----------------
**2.1 - Modifiers**

*Methods that will be modify rows, (INSERT, UPDATE, DELETE)*
 
 Eg.:
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


Instead of use this very old and ugly form:


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

And its the same of UPDATE and DELETE methods, just changing the parameters and query of course!


**2.2 Retrieves**

To SELECT query statements you need one more method in your type class.
There is no pattern about the name. Here I just call him, FillAttributes;
He'll be used like as a Func&lt;DataRow&gt;.


See:
```
protected override Tag FillAttributes(DataRow dr)
{
  return new Tag
  {
    ID = dr.Verify("id", Convert.ToInt32),
    Name = dr.Verify("name", Convert.ToString),
    Uri = dr.Verify("uri", Convert.ToString),
    TotalPosts = dr.Verify("total_posts", Convert.ToString) 
  };
}
```

And you'll need him on these code (kept at tag.cs!)

```
public List<Tag> List(int now, int end)
{
  try
  {
    string strSQL = @"SELECT * FROM tbl_catalogs ORDER BY id DESC LIMIT @now, @end;";
    return DataRepository.List(new Dictionary<string, object>() { {"now", now}, {"end", end} },
                               strSQL, FillAttributes);
  }
  catch (Exception e)
  {
    throw new Exception(e.Message);
}
```

**2.2.1 Extension Method**

The method exemplicated before this explanation, verify all of collumns that comes into the DataRow object. In this form, you need define only once the method `FillAtributes`.
