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
    public Category Parent { get; set; }
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
Each of your values of a type ```T``` are represented as a ```ITreeNode<T>``` which provides the navigation. You can retrieve the value represented by the node using the ```Value``` property.

*Note: TX operates on in-memory objects and not on the rows of a SQL table or any other remote resource.*

#### Parent and child relations
You can get the parent node using the ```Parent()``` method and children using the ```Children()``` method.

#### Descendants, ancestors and siblings


#### Roots and leaves
You can determine whether a node is a root by ```IsRoot()```. Get the root of a set of nodes by calling ```Root()```, which requires that the set of nodes has exactly one root.

To get all leaf nodes of a sub-tree use ```Leaves()```.

## Portability
* Portable Class Library which provides support for .NET Framework 4, Silverlight 5, Windows 8, Windows Phone 8.1, Windows Phone Silverlight 8
* Common Language Specification compliant code

## Code Quality
* Passes Microsoft Managed Recommended Rules without any warnings
* All public members and parameters are documented