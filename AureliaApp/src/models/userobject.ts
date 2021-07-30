export class UserObject {
    constructor(age: number, firstName: string, lastName: string, address: string, email: string) {
        this.Age = age;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Address = address;
        this.Email = email;
    }
    Age: number;
    AgeValid(): boolean {

        return this.Age > 18;
    }
    FirstName: string;
    FirstNameValid(): boolean {
        if (this.FirstName) {
            return this.FirstName.length >= 3;
        }
        return false;
    }
    LastName: string;
    LastNameValid(): boolean {
        if (this.LastName) {
            return this.LastName.length >= 3;
        }
        return false;
    }
    Address: string;
    AddressValid(): boolean {
        if (this.Address) {
            return /.+\d+\s*\,*\s*\d{5}.*/.test(this.Address);
        }
        return false;
    }
    Email: string;
    EmailValid(): boolean {
        if (this.Email) {
            return /.+@{1}\w+\.{1}\w{2,}/.test(this.Email);
        }
        return false;
    }
}