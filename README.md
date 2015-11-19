# Tree Extensions
Tree Extensions is a .NET library which provides LINQ-like operators for querying any tree-like structure.
 
## Getting started

### Realizing trees

#### Foreign keys
Trees can be defined by foreign keys. Let's suppose that you have a domain entity like this one:
```C#
public class Category
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int ParentId { get; set; }
}
```

Then you can realize a set of categories as a set of tree nodes:
```C#
catgories.ToTree(c => c.Id, c => c.ParentId);
```

If you have a well-defined root of your tree, you can get it by the ```Root()``` method:
```C#
ITreeNode<T> root = catgories.ToTree(c => c.Id, c => c.ParentId).Root();
```

#### Custom selectors
TO DO

### Querying trees
Each of your values of a ```T``` type are represented as a ```ITreeNode<T>``` which provides the navigation. You can retrieve the value represnted by the node using the ```Value``` property.

*Note: TX operates on in-memory objects and on the rows of a SQL table or any other remote resource.*

#### Parent and child relations
You can get the parent node using the ```Parent()``` method and children using the ```Children()``` method.

#### Descendants and ancestors


#### Roots and leaves

## Portability
TO DO

## Code Quality
TO DO
