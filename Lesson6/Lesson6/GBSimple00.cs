using Autofac;
using Autofac.Features.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6
{
    internal class GBSimple00
    {
        static void Main(string[] args) 
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<FirstPipelineItem>().As<IPipelineItem>().WithMetadata("Name", "First pipe item");
            builder.RegisterType<SecondPipelineItem>().As<IPipelineItem>().WithMetadata("Name", "Second pipe item");
            builder.RegisterAdapter<Meta<IPipelineItem>, Operation>(
                cmd => new Operation(cmd.Value, (string)cmd.Metadata["Name"]));

            IContainer container = builder.Build();

            IReadOnlyList<Operation> operations = container.Resolve<IEnumerable<Operation>>().ToList();
            foreach (Operation operation in operations) 
            { 
                Console.WriteLine($"Operation name is {operation.Name}"); 
                operation.Execute(); 
            }
        }

        public interface IPipelineItem 
        {
            string Name { get; } 
            void Run(); 
        }

        public sealed class FirstPipelineItem : IPipelineItem
        { 
            public string Name => $"{nameof(FirstPipelineItem)}"; 
            public void Run() 
            { 
                Console.WriteLine($"Hello from {Name}"); 
            } 
        }
        
        public sealed class SecondPipelineItem : IPipelineItem
        { 
            public string Name => $"{nameof(SecondPipelineItem)}"; 
            public void Run() 
            { 
                Console.WriteLine($"Hello from {Name}"); 
            } 
        }
        
        public sealed class Operation 
        { 
            private readonly IPipelineItem _pipelineItem; 
            private readonly string _name; 
            public Operation(IPipelineItem pipelineItem, string name) 
            { 
                _pipelineItem = pipelineItem;
                _name = name; 
            } 
            public string Name => _name; 
            public void Execute() 
            { 
                _pipelineItem.Run(); 
            } 
        }
    }
}
