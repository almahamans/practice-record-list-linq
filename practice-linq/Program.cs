//proplem 1
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.VisualBasic;

public record Contact(string Name, string PhoneNumber, string Email);
class ContactManager{
    List<Contact> contacts = new List<Contact>();
    public void AddContact(Contact contact){
        contacts.Add(contact);
        Console.WriteLine($"Contact {contact.Name} Added successfully");
    }
    public void RemoveContact(string name){
        Console.WriteLine($"Remove {name} from contact list");
        contacts.Remove(FindContact(name));
        Console.WriteLine($"Removing {name} from contact list successfully");
    }
    public Contact? FindContact(string name){
        Console.WriteLine($"Finding contact name {name}:");
        Console.WriteLine($"{contacts.FirstOrDefault(i => i.Name == name)}");
        return contacts.FirstOrDefault(i => i.Name == name);
    }
    public void DisplayContact(){
        Console.WriteLine($"Contact List:");
        foreach(var item in contacts){
            Console.WriteLine($"{item}");
        }   
    }
}
//problem 2
public record Student(string Name, double Grade, string Subject);
class StudentManager {
    List<Student> students = new List<Student>();
    public void AddStudent(Student name){
        // Console.WriteLine($"Student name {name} added successfully");
        students.Add(name);
    }
    public Student? FindStudent(string name){
        Console.WriteLine($"{students.Find(i => i.Name == name)}");
        return students.Find(i => i.Name == name);
    }
    public void RemoveStudent(string name){
        Console.WriteLine($"Student removed successfully");
        students.Remove(FindStudent(name));
    }
    public void DisplayStudent(){
    Console.WriteLine($"Student List:");
    foreach(var person in students){
        Console.WriteLine($"{person}");
    }   
    }
}
//problem 3
public record Book(int Id, string Title, string Author, string Gener, bool IsAvailable = true);
public record Member(int Id, string Name, string Email);
public record BorrowRecord(DateTime date, string Title, string BorrowedName);

class LibraryManager{
    List<Book> books = new List<Book>();
    List<Member> members = new List<Member>();
    List<BorrowRecord> borrowRecords = new List<BorrowRecord>();
    public void AddBooks(Book book){
        Console.WriteLine($"Book added successfully");
        books.Add(book);
    }
    public void AddMembers(Member member){
        Console.WriteLine($"Member added successfully");
        members.Add(member);
    }
    public void AddBorrow(string bName, string title){
        BorrowRecord br = new BorrowRecord(DateTime.Now, title, bName);
        borrowRecords.Add(br);
        Console.WriteLine($"Book {title} borrowed by {bName}");
    }
    public void DisplayBooks(){
        foreach(var i in books){
         Console.WriteLine($"{i.Id}, {i.Title}, {i.Author}, {i.Gener}, {i.IsAvailable}");   
        } 
    }
    public void DisplayMembers(){
        foreach(var i in members){
         Console.WriteLine($"{i.Id}, {i.Name}, {i.Email}");   
        } 
    }
    public Book? FindBooks(string title){
        return books.Find(i => i.Title == title);
    }
//search on member and what books she/he borrow
    public void BorrowedBookByMember(string name){
        Console.WriteLine($"{borrowRecords.Find(i => i.BorrowedName == name)?.BorrowedName} borrowed {borrowRecords.Find(i => i.BorrowedName == name)?.Title}");
    }
//check if the book returned or not
    public Book? CheckReturnedBook(Book book){
        var x = borrowRecords.Where(i => i.Title == book.Title);
        Book b = book;
        if(x != null && book.IsAvailable == false){
            Console.WriteLine($"Book {book.Title} returned by {borrowRecords.FirstOrDefault(i => i.BorrowedName != null)?.BorrowedName}");
            b = book with {IsAvailable = true};
        }else{
            Console.WriteLine($"Book {book.Title} not returned");
        }
        return b;
    }
    public void DisplayBorrowedBooks(){
        Console.WriteLine($"Borrow Records:");  
        foreach(var i in borrowRecords){
         Console.WriteLine($"{i.Title} borrowed by {i.BorrowedName} on {i.date}");    
        }    
    }
}


class Program{
    public static void Main(string[] args)
    {   
        // ContactManager cm = new ContactManager();
        // Contact cm1 = new Contact("almaha", "000000000", "jjjjjj");
        // cm.AddContact(cm1);
        // Contact cm2 = new Contact("hadeel", "000000000", "jjjjjj");
        // cm.AddContact(cm2);

        // cm.DisplayContact();
        // cm.RemoveContact("hadeel");
        // Console.WriteLine($"Display contact after removing:");     
        // cm.DisplayContact();
        // cm.FindContact("almaha");

        ////////////////////////
        // StudentManager sm = new StudentManager();

        // Student student = new Student("almaha", 9.50, "math");
        // sm.AddStudent(student);
        // Student student2 = new Student("raghad", 9.50, "math");
        // sm.AddStudent(student2);
        
        // sm.DisplayStudent();
        // sm.FindStudent("almaha");
        // sm.RemoveStudent("almaha");
        // sm.DisplayStudent();

        /////////////////
        LibraryManager lm = new LibraryManager();
        Book book1 = new Book(1, "The Great Gatsby", "F. Scott Fitzgerald", "Fiction");
        lm.AddBooks(book1);
        Book book2 = new Book(2, "1984", "George Orwell", "Dystopian");
        lm.AddBooks(book2);
        Book book3 = new Book(3, "Robin hode", "George Orwell", "Dystopian");
        lm.AddBooks(book3);
        lm.DisplayBooks();
        Member m1 = new Member(1, "almaha", "email.org.sa");
        lm.AddMembers(m1);
        Member m2 = new Member(2, "raghad", "email.com");
        lm.AddMembers(m2);
        Member m3 = new Member(3, "dania", "email.com");
        lm.AddMembers(m3);
        lm.DisplayMembers();
        lm.AddBorrow(m2.Name, book1.Title);
        lm.AddBorrow(m3.Name, book3.Title);
        lm.DisplayBorrowedBooks();
        
        lm.DisplayBooks();
        
        Book bok1 = book1 with {IsAvailable = false};
        Book bok3 = book3 with {IsAvailable = false};
        Console.WriteLine($"Display books after borrowing:");
        Console.WriteLine(bok1);
        Console.WriteLine(book2);
        Console.WriteLine(bok3);
        
        lm.CheckReturnedBook(bok1);
        var m = lm.CheckReturnedBook(bok1);
        
        lm.BorrowedBookByMember("dania");
        
    }
}
