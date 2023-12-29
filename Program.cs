using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 1 for see todos");
            Console.WriteLine("Press 2 for new todo ");
            Console.WriteLine("Press 3 for completing the task");
            Console.WriteLine("Press 4 for deleting the task");
            Console.WriteLine("Press 0 for exit");
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Enter Your choice");
                string ch = Console.ReadLine();
                DataClasses1DataContext dtContext = new DataClasses1DataContext();
                Todo todo = new Todo();
                switch (ch)
                {
                    case "1":
                        viewDotos(dtContext);
                        break;
                    case "2":
                        Console.WriteLine("Enter Todo");
                        var data= Console.ReadLine();
                        todo.Name = (string)data;
                        todo.IsDone = false;
                        dtContext.Todos.InsertOnSubmit(todo);
                        dtContext.SubmitChanges();    
                        Console.WriteLine("Todo added successfully");
                        break;
                    case "3":
                        Console.WriteLine("Enter id for which you want to complete the task");
                        int id = int.Parse(Console.ReadLine());
                        Todo todoUpdate=dtContext.Todos.SingleOrDefault(x => x.Id == id);
                        if (todoUpdate != null)
                        {
                            todoUpdate.IsDone = true;
                            dtContext.SubmitChanges();
                        }
                        Console.WriteLine("Todo is marked successfully");
                        dtContext = new DataClasses1DataContext();
                        break;
                    case "4":
                        Console.WriteLine("Enter id for which you want to delete the task");
                         id = int.Parse(Console.ReadLine());
                        Todo todoDelete = dtContext.Todos.SingleOrDefault(x => x.Id == id);
                        if (todoDelete != null)
                        {
                            dtContext.Todos.DeleteOnSubmit(todoDelete);
                            dtContext.SubmitChanges();
                        }
                        Console.WriteLine("Task deleted successfully");
                        break;
                    case "0":
                        flag= false;
                        Console.WriteLine("Press any key to exit");
                        break;
                    default:
                        Console.WriteLine("Enter correct choice");
                        break;
                }
            }
            Console.ReadKey();  
        }

        private static void viewDotos(DataClasses1DataContext dtContext)
        {
            var todos = from row in dtContext.Todos select row;
            Console.WriteLine("Id---Name-----IsDone");
            foreach (var t in todos)
            {
                Console.WriteLine($"{t.Id}----{t.Name}-----{((bool)t.IsDone ? "Done" : "Pending")}");
            }
        }
    }
}
