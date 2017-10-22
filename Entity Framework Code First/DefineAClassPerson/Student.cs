namespace DefineAClassPerson
{
    class Student
    {
        public static int count;
        public Student(string name)
        {
            this.Name = name;
            count++;
        }

        public string Name { get; set; }
    }
}
