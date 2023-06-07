using System;

namespace RefactoringGuru.DesignPatterns.AbstractFactory.Conceptual
{

    // La interfaz de Abstract Factory declara un conjunto de métodos que devuelven
    // diferentes productos abstractos. Estos productos se denominan familia y son
    // relacionados por un tema o concepto de alto nivel. Los productos de una familia son
    // por lo general capaces de colaborar entre ellos. Una familia de productos puede
    // tener varias variantes, pero los productos de una variante son incompatibles
    // con productos de otro.

    public interface IAbstractFactory
    {
        IAbstractProductA CreateProductA();

        IAbstractProductB CreateProductB();
    }

    // Las Fábricas Concreto producen una familia de productos que pertenecen a un solo
    // variante. La fábrica garantiza que los productos resultantes son compatibles.
    // Tenga en cuenta que las firmas de los métodos de Concrete Factory devuelven un resumen
    // producto, mientras que dentro del método se instancia un producto concreto.

    class ConcreteFactory1 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ConcreteProductA1();
        }

        public IAbstractProductB CreateProductB()
        {
            return new ConcreteProductB1();
        }
    }

   // Cada Concrete Factory tiene una variante de producto correspondiente.

    class ConcreteFactory2 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ConcreteProductA2();
        }

        public IAbstractProductB CreateProductB()
        {
            return new ConcreteProductB2();
        }
    }

   // Cada producto distinto de una familia de productos debe tener una interfaz base.
   // Todas las variantes del producto deben implementar esta interfaz.

    public interface IAbstractProductA
    {
        string UsefulFunctionA();
    }

   // Los Productos de Concreto son creados por las Fábricas de Concreto correspondientes.

    class ConcreteProductA1 : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "El resultado del producto A1.";
        }
    }

    class ConcreteProductA2 : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "El resultado del producto A2.";
        }
    }

    // Aquí está la interfaz base de otro producto. Todos los productos pueden
    // interactuar entre sí, pero la interacción adecuada solo es posible entre
    // productos de la misma variante concreta.

    public interface IAbstractProductB
    {
        // El Producto B es capaz de hacer lo suyo...
        string UsefulFunctionB();
        // ...pero también puede colaborar con el ProductoA.
        //
        // El Abstract Factory se asegura de que todos los productos que crea sean de
        // la misma variante y por lo tanto, compatible.
        string AnotherUsefulFunctionB(IAbstractProductA collaborator);
    }

    // Los Productos de Concreto son creados por las Fábricas de Concreto correspondientes.
    class ConcreteProductB1 : IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "El resultado del producto B1.";
        }

        // La variante, Producto B1, solo puede funcionar correctamente con el
        // variante, Producto A1. Sin embargo, acepta cualquier instancia de
        // AbstractProductA como argumento.
        public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"El resultado de la colaboración del B1 con el ({result})";
        }
    }

    class ConcreteProductB2 : IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "El resultado del producto B2.";
        }

       // La variante, Producto B2, solo puede funcionar correctamente con el
       // variante, Producto A2. Sin embargo, acepta cualquier instancia de
       // AbstractProductA como argumento.
        public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"El resultado de la colaboración de B2 con el ({result})";
        }
    }

    // El código del cliente funciona con fábricas y productos solo a través de resumen
    // tipos: AbstractFactory y AbstractProduct. Esto le permite pasar cualquier
    // subclase de fábrica o producto al código del cliente sin romperlo.
    class Client
    {
        public void Main()
        {
            // El código del cliente puede funcionar con cualquier clase de fábrica concreta.
            Console.WriteLine("Cliente: Probando el código del cliente con el primer tipo de fábrica...");
            ClientMethod(new ConcreteFactory1());
            Console.WriteLine();

            Console.WriteLine("Cliente: Probando el mismo código de cliente con el segundo tipo de fábrica...");
            ClientMethod(new ConcreteFactory2());
        }

        public void ClientMethod(IAbstractFactory factory)
        {
            var productA = factory.CreateProductA();
            var productB = factory.CreateProductB();

            Console.WriteLine(productB.UsefulFunctionB());
            Console.WriteLine(productB.AnotherUsefulFunctionB(productA));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Client().Main();
        }
    }
}