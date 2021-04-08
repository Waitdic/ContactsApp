import { Component, OnInit} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { DataService } from './data.service';
import { Contact } from './contact';
import { checkServerIdentity } from 'tls';
 
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

    constructor(private dataService: DataService,) { }
 
    ngOnInit() {
        this.loadContacts(); 
    }
    
    loadContacts() {
        this.dataService.getContacts()
            .subscribe((data: Contact[]) => this.contacts = data);
    }
   
    save() {
        if (this.contact.id == null) {
            this.dataService.addContact(this.contact)
                .subscribe((data: Contact[]) => this.contacts = data);
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

    search(str: String) {
       this.dataService.search(str).subscribe((data: Contact[]) => this.contacts = data);
      }

   add() {
        this.cancel();
        this.tableMode = false;
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
}