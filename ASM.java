import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

class Student {
    private String firstName;
    private String lastName;

    public Student(String firstName, String lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }

    public String getFullName() {
        return lastName + " " + firstName;
    }

    public void updateName(String firstName, String lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }

    public boolean matchesFullName(String fullName) {
        return getFullName().equalsIgnoreCase(fullName);
    }

    public boolean matchesLastName(String lastName) {
        return this.lastName.equalsIgnoreCase(lastName);
    }
}

class StudentManagerApp {
    private static final List<Student> students = new ArrayList<>();

    public static void main(String[] args) {
        try (Scanner scanner = new Scanner(System.in)) {
            while (true) {
                displayMenu();
                int choice = promptForInt(scanner, "Enter your choice: ");

                switch (choice) {
                    case 1 -> addStudents(scanner);
                    case 2 -> searchByLastName(scanner);
                    case 3 -> updateStudentByFullName(scanner);
                    case 4 -> {
                        System.out.println("Program terminated. Goodbye!");
                        return;
                    }
                    default -> System.out.println("Invalid choice. Please try again.");
                }
            }
        }
    }

    private static void displayMenu() {
        System.out.println("\n=== Student Manager ===");
        System.out.println("1. Add Students");
        System.out.println("2. Search Students by Last Name");
        System.out.println("3. Edit a Student by Full Name");
        System.out.println("4. Exit");
    }

    private static int promptForInt(Scanner scanner, String message) {
        while (true) {
            System.out.print(message);
            try {
                return Integer.parseInt(scanner.nextLine().trim());
            } catch (NumberFormatException e) {
                System.out.println("Invalid input. Please enter a valid integer.");
            }
        }
    }

    private static String promptForNonEmptyString(Scanner scanner, String message) {
        while (true) {
            System.out.print(message);
            String input = scanner.nextLine().trim();
            if (!input.isEmpty()) {
                return input;
            }
            System.out.println("Input cannot be empty. Please try again.");
        }
    }

    private static void addStudents(Scanner scanner) {
        int numOfStudents = promptForInt(scanner, "Enter the number of students to add: ");
        for (int i = 1; i <= numOfStudents; i++) {
            System.out.println("Student " + i + ":");
            String firstName = promptForNonEmptyString(scanner, "  First Name: ");
            String lastName = promptForNonEmptyString(scanner, "  Last Name: ");
            students.add(new Student(firstName, lastName));
        }
        System.out.println("Students have been successfully added.");
    }

    private static void searchByLastName(Scanner scanner) {
        String lastName = promptForNonEmptyString(scanner, "Enter last name to search: ");
        List<Student> matchingStudents = students.stream()
                .filter(student -> student.matchesLastName(lastName))
                .toList();

        if (matchingStudents.isEmpty()) {
            System.out.println("No students found with last name: " + lastName);
        } else {
            System.out.println("Students with last name '" + lastName + "':");
            matchingStudents.forEach(student -> System.out.println("  - " + student.getFullName()));
        }
    }

    private static void updateStudentByFullName(Scanner scanner) {
        String fullName = promptForNonEmptyString(scanner, "Enter full name of the student to edit (Last Name First Name): ");
        Student studentToUpdate = students.stream()
                .filter(student -> student.matchesFullName(fullName))
                .findFirst()
                .orElse(null);

        if (studentToUpdate == null) {
            System.out.println("No student found with the name: " + fullName);
            return;
        }

        System.out.println("Editing details for: " + fullName);
        String newFirstName = promptForNonEmptyString(scanner, "  New First Name: ");
        String newLastName = promptForNonEmptyString(scanner, "  New Last Name: ");
        studentToUpdate.updateName(newFirstName, newLastName);
        System.out.println("Student details updated successfully.");
    }
}