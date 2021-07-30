import { UserObject } from '../models/userobject';
import { DataEDit } from '../uibases/dataedit';
import {ValidationRules} from 'aurelia-validation';

export class User extends DataEDit<UserObject>{
    activate(params) {
        this.controlName = 'Users';
        if (params.email) {
            this.currentId = params.email;
            this.isNew = false;
        } else {
            this.isNew = true;
            this.currentId = '';
        }
        
    }
    override createNewData(): void {
        this.data = new UserObject(0, '', '', '', '');
        this.backUpData = new UserObject(0, '', '', '', '');
    }
    override updateData(data: any): void {
        this.data.Address = data.address;
        this.data.Age = data.age;
        this.data.Email = data.email;
        this.data.FirstName = data.firstName;
        this.data.LastName = data.lastName;
    }
    override validate(): boolean {
        let b: boolean = this.data.AgeValid() && this.data.EmailValid() && this.data.AddressValid()
            && this.data.LastNameValid() && this.data.FirstNameValid();
        return b;
    }
    override copyData(source: UserObject, target: UserObject): void {
        target.Address = source.Address;
        target.Age = source.Age;
        target.Email = source.Email;
        target.FirstName = source.FirstName;
        target.LastName = source.LastName;

    }
    override isChanged(): boolean {
        let b: boolean = this.data.Address != this.backUpData.Address
            || this.data.Age != this.backUpData.Age
            || this.data.Email != this.backUpData.Email
            || this.data.FirstName != this.backUpData.FirstName
            || this.data.LastName != this.backUpData.LastName;
        return b;
    }
    override setValidation() {
        ValidationRules
            .ensure('FirstName').displayName('firstname').required().minLength(3)
            .ensure('LastName').displayName('lastname').required().minLength(3)
            .ensure('Age').displayName('age').required().min(19)
            .ensure('Email').displayName('email').required().matches(/.+@{1}\w+\.{1}\w{2,}/).withMessage(this.i18n.tr("errorMessages.email_form"))
            .ensure('Address').displayName('address').required().matches(/.+\d+\s*\,*\s*\d{5}.*/).withMessage(this.i18n.tr("errorMessages.address_form"))
            .on(this.data);
    }

}