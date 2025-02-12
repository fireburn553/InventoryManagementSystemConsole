// Struct for Product Details
struct ProductDetails
{ // This struct will store the product details such as name, brand, quantity, and price of the product
    public string Name { get; set; } // Properties to store the product name
    public string Brand { get; set; } // Properties to store the product brand
    public int Quantity { get; set; } // Properties to store the product quantity
    public decimal Price { get; set; } // Properties to store the product price

    public ProductDetails(string name, string brand, int quantity, decimal price)
    { // Constructor to initialize the product details
        Name = name;
        Brand = brand;
        Quantity = quantity;
        Price = price;
    }
}

// Abstract class for Products
abstract class Product
{ // This class will store the product details and display the product details
    public ProductDetails Details { get; set; } // Property to store the product details

    public Product(string name, string brand, int quantity, decimal price)
    { // Constructor to initialize the product details
        Details = new ProductDetails(name, brand, quantity, price); // Initialize the product details
    }

    public abstract void DisplayInfo(); // Abstract method to display product details
}

// Derived class for Electronic Products
class ElectronicProduct : Product // This class will store the electronic product details and display the electronic product details inherited from the Product class
{
    public ElectronicProduct(string name, string brand, int quantity, decimal price)
        : base(name, brand, quantity, price) { }

    public override void DisplayInfo() // override the DisplayInfo method from the base class
    { // This method will display the product details of the electronic product
        Console.WriteLine($"Product Name: {Details.Name}");
        Console.WriteLine($"Brand Name: {Details.Brand}");
        Console.WriteLine($"Quantity: {Details.Quantity}");
        Console.WriteLine($"Price: {Details.Price:C}");
    }
}

// Inventory Management System
class Inventory
{ // This class will manage the inventory of the products
    private List<Product> products = new List<Product>(); // List to store products

    public void AddProduct(Product product) // This method will add the product to the inventory 
    {
        products.Add(product); // Add product to the list 
        Console.WriteLine("✅ Product added successfully."); // Display message that product has been added
    }

    public void RemoveProduct(int index)
    { // This method will remove the product from the inventory
        if (index >= 0 && index < products.Count) // Check if index is within product range
        {
            products.RemoveAt(index); // Remove product at index
            Console.WriteLine("❌ Product removed successfully.");
        }
        else
        {
            Console.WriteLine("⚠️ Invalid product number."); // If index is invalid and also for validation and handling error
        }
    }

    public void UpdateProduct(int index)
    { // This method will update the product details
        if (index >= 0 && index < products.Count) // Check if index is within product range
        {
            Product product = products[index]; // Get product at index
            ProductDetails details = product.Details; // Get product details

            while (true)    // Loop to prevent exiting on invalid input
            {
                Console.WriteLine($"\nUpdate {details.Name}:"); // Display product name to update
                Console.WriteLine("[1] Product Name"); // Options to update product name
                Console.WriteLine("[2] Brand Name"); // Options to update brand name
                Console.WriteLine("[3] Quantity");  // Options to update quantity
                Console.WriteLine("[4] Price"); // Options to update price
                Console.WriteLine("[0] Back to Product Details"); // Options to go back to product details
                Console.Write("🔹 Choose an option: ");

                string choice = Console.ReadLine(); // Read user input
                switch (choice) // I use switch statement to check the user input
                {
                    case "1": // If user input is 1 update product name
                        Console.Write("Enter updated product name: ");
                        details.Name = Console.ReadLine(); // Read user input and update product name
                        Console.WriteLine("✅ Your product name has been updated!");
                        break; // Break the switch statement
                    case "2": // If user input is 2 update brand name
                        Console.Write("Enter updated brand name: ");
                        details.Brand = Console.ReadLine(); // Read user input and update brand name
                        Console.WriteLine("✅ Your brand name has been updated!");
                        break; // break the switch statement
                    case "3":
                        Console.Write("Enter updated quantity: ");
                        details.Quantity = int.Parse(Console.ReadLine()); // Read user input and update quantity
                        Console.WriteLine("✅ Your quantity has been updated!");
                        break; // Break the switch statement
                    case "4":
                        Console.Write("Enter updated price: ");
                        details.Price = decimal.Parse(Console.ReadLine()); // Read user input and update price
                        Console.WriteLine("✅ Your price has been updated!");
                        break; // break the switch statement
                    case "0":
                        product.Details = details; // Update struct inside the product
                        return;
                    default: // default case if user input is invalid also for handling user input error
                        Console.WriteLine("❌ Invalid choice. Try again.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("⚠️ Invalid product number."); // If index is invalid and also for validation and handling error
        }
    }

    // DisplayInventory Method
    public void DisplayInventory()
    { // This method display the current inventory status. If there is no product in the inventory it will show "No inventory" and if there is product it will show the product details. Also included in this method is the option to expand the product details, remove product, and update product.
        while (true) // Loop to prevent exiting on invalid input
        {
            //Console.Clear(); // Clear the console screen
            if (products.Count == 0) // if there is no inventory then the program will show "No inventory" and will return to the main menu 
            {
                Console.WriteLine("\n📦 Inventory");
                Console.WriteLine("No inventory");
                Console.WriteLine("[0] Back to Main Menu");
                Console.Write("🔹 Choose an option: ");

                string input = Console.ReadLine();
                if (input == "0") return; // Return to main menu if input is "0"

                Console.WriteLine("❌ Invalid choice. Try again.");
                continue; // Restart loop
            }

            Console.WriteLine("\n📦 Inventory:"); // print the inventory if the inventory has product
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("{0,-5} | {1,-15} | {2,-10} | {3,-10} | {4,-10}", "No", "Product", "Brand", "Quantity", "Price"); // print the header of the inventory and format the text
            Console.WriteLine("----------------------------------------------------------------");

            for (int i = 0; i < products.Count; i++) // loop for the product list
            {
                Console.WriteLine("{0,-5} | {1,-15} | {2,-10} | {3,-10} | {4,10:C}",
                    i + 1, products[i].Details.Name, products[i].Details.Brand, products[i].Details.Quantity, products[i].Details.Price); // uses a format string to format the text and display the product details
            }

            Console.WriteLine("----------------------------------------------------------------");
            Console.Write("Enter the product number to expand details or enter 0 to go back: ");

            if (int.TryParse(Console.ReadLine(), out int choice)) // Try to parse user input to integer choice variable
            {
                if (choice == 0) return; // Exit inventory view
                if (choice > 0 && choice <= products.Count) // Check if choice is within product range (1 to products.Count)
                { // Show product details if choice is valid
                    ShowProductDetails(choice - 1); // Pass index of product to show details of the product
                    continue; // After viewing details, restart inventory loop
                }
            }
            Console.WriteLine("❌ Invalid choice. Try again.");
        }
    }

    private void ShowProductDetails(int index) // ShowProductDetails Method to show the product details and options to remove or update the product 
    {
        while (true) // Loop to prevent exiting on invalid input
        {
            //Console.Clear();
            products[index].DisplayInfo(); // Display product details
            Console.WriteLine("\n[1] Remove Product"); // Option to remove product
            Console.WriteLine("[2] Update Product"); // Option to update product
            Console.WriteLine("[3] Back to Inventory"); // Option to go back to inventory 
            Console.Write("🔹 Choose an option: ");

            string choice = Console.ReadLine(); // Read user input 
            switch (choice) // I use switch statement to check the user input
            {
                case "1":
                    RemoveProduct(index); // Call RemoveProduct method to remove the product
                    return;
                case "2":
                    UpdateProduct(index); // Call UpdateProduct method to update the product
                    break;
                case "3":
                    return;
                default:    // This is the default case if user input is invalid also for handling user input error
                    Console.WriteLine("❌ Invalid choice. Try again.");
                    break;
            }
        }
    }
}

class Program // Main Program
{
    static void Main() // Main Method
    {
        Inventory inventory = new Inventory(); // Create an instance of Inventory class

        while (true) // Main Menu Loop
        {
            //Console.Clear(); // Clear the console screen 
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("📋 Inventory Management System");
            Console.WriteLine("[1] Go to Inventory");
            Console.WriteLine("[2] Add Product");
            Console.WriteLine("[3] Exit");
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("🔹 Choose an option: ");

            string choice = Console.ReadLine(); // Read user input
            switch (choice) // I use switch statement to check the user input 
            {
                case "1": // If user input is 1 Go to Inventory
                    //Console.Clear(); // Clear the console screen
                    inventory.DisplayInventory();   // Call DisplayInventory method
                    break; // Break the switch statement
                case "2": // If user input is 2 Add Product
                    Console.Write("Enter product name: "); // Ask user to enter product name
                    string name = Console.ReadLine();   // Read user input
                    Console.Write("Enter brand: "); // Ask user to enter brand
                    string brand = Console.ReadLine();  // Read user input
                    Console.Write("Enter quantity: ");  // Ask user to enter quantity
                    int quantity = int.Parse(Console.ReadLine());   // Read user input
                    Console.Write("Enter price: ");     // Ask user to enter price
                    decimal price = decimal.Parse(Console.ReadLine()); // Read user input

                    Product product = new ElectronicProduct(name, brand, quantity, price); // I created an instance of ElectronicProduct class
                    inventory.AddProduct(product); // Call AddProduct method to add the product 
                    break; // Break the switch statement
                case "3": // If user input is 3 Exit
                    Console.WriteLine("👋 Exiting program...");
                    return;     // Exit the program
                default: // This is the default case if user input is invalid also for handling user input error
                    Console.WriteLine("❌ Invalid choice. Try again."); // If user input is invalid
                    break;
            }
        }
    }
}
