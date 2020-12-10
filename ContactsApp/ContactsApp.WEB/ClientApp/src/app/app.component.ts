import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Contact } from './contact';
 
@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [DataService]
})
export class AppComponent implements OnInit {
 
    contact: Contact = new Contact();   
    contacts: Contact[];               
    tableMode: boolean = true;          
 
    constructor(private dataService: DataService) { }
 
    ngOnInit() {
        this.loadContacts();    // загрузка данных при старте компонента  
    }
    // получаем данные через сервис
    loadContacts() {
        this.dataService.getContacts()
            .subscribe((data: Contact[]) => this.contacts = data);
    }
    // сохранение данных
    save() {
        if (this.contact.id == null) {
            this.dataService.addContact(this.contact)
                .subscribe((data: Contact) => this.contacts.push(data));
        } else {
            this.dataService.editContact(this.contact)
                .subscribe(data => this.loadContacts());
        }
        this.cancel();
    }
    editContact(p: Contact) {
        this.contact = p;
    }
    cancel() {
        this.contact = new Contact();
        this.tableMode = true;
    }
    delete(p: Contact) {
        this.dataService.deleteContact(p.id)
            .subscribe(data => this.loadContacts());
    }
    add() {
        this.cancel();
        this.tableMode = false;
    }
}