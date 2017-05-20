namespace IDisposablePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // Just a note:
            // When using "using" statements , this gets compiled to IL try/finally block
            // which executes virtual method Dispose() to the object created in using.
            using (var connection = new DatabaseState())
            {
                connection.GetDate();
            }
        }
    }
}
