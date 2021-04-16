import { Component, OnInit} from '@angular/core';
import { DataService } from './data.service';
import { Contact } from './contact';
 
@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [DataService],
    styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit  {
    contact: Contact = new Contact();   
    contacts: Contact[];         
    tableMode: boolean = true;
    searchString: string;
    originalContacts: Contact[]; 

    constructor(private dataService: DataService,) { }
 
    ngOnInit() {
        this.loadContacts(); 
    }
    
    loadContacts() {
        this.dataService.getContacts()
            .subscribe((data: Contact[]) => {
                this.contacts = data; 
                this.originalContacts = data
            });
        }
   
    save() {
        if (this.contact.id == null) {
            this.dataService.addContact(this.contact)
                .subscribe((data: Contact[]) => {
                    this.contacts = data; 
                    this.originalContacts = data
                });
        } else {
            this.dataService.editContact(this.contact)
                .subscribe(data => this.loadContacts());
        }
        this.cancel();
    } 

    editContact() {
        if(this.contact !== null)
        {
            this.tableMode = false;
        }
    }

    showContact(p: Contact) {
      this.contact = p;
    }

    search() {
       if (this.originalContacts !== null) {
            this.contacts = [];
            this.originalContacts.forEach(o => {
                var substr = this.searchString.replace(/\s/g, "").toLocaleLowerCase();
                if ((o.name + o.surname).replace(/\s/g, "").toLowerCase().includes(substr)) {
                    this.contacts.push(o);
                }
            });
        }
    }

   add() {
        this.cancel();
        this.tableMode = false;
    }

    addMany() {
        for (var i = 0; i < 220; i++){
            var date = new Date();
            date.setDate(date.getDate()-100);
            var num = this.contacts.length > 0 ? this.contacts[this.contacts.length-1].id + 1 : 0;
            var newContact = new Contact(
                null,
                "TestName" + num,
                "TestSurname" + num,
                date,
                "89235371957",
                "waitdic161616@yandex.ru",
                "123"
            );

            this.dataService.addContact(newContact)
            .subscribe((data: Contact[]) => {
                this.contacts = data; 
                this.originalContacts = data
            });
        }
        this.cancel();
    }

    cancel() {
        this.contact = new Contact();
        this.tableMode = true;
    }

    delete() {
        if (this.contact.id)
        {
            this.dataService.deleteContact(this.contact.id)
                .subscribe(data => this.loadContacts());
            this.contact = new Contact();
        }
    }

    deleteMany() {
        this.contacts.forEach(e => {
            this.dataService.deleteContact(e.id)
                .subscribe(data => this.loadContacts());
            this.contact = new Contact();
        });
    }
}