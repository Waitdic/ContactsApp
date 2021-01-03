import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { DataService } from './data.service';
import { Contact } from './contact';
import { ModalComponent } from './modal/modal.component';
 
@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [DataService],
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
 
    contact: Contact = new Contact();   
    contacts: Contact[];               
    tableMode: boolean = true;              
    dataForm: FormGroup;

    constructor(
        private dataService: DataService,
        private modalComponent: ModalComponent) { }
 
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
                .subscribe((data: Contact[]) => this.contacts = data);
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
        this.dataForm = new FormGroup({firstName: new FormControl('')})
        this.contact = new Contact();
    }
    delete() {
        if (this.contact.id)
        {
            this.dataService.deleteContact(this.contact.id)
                .subscribe(data => this.loadContacts());
            this.contact = new Contact();
        }
    }

    openModal() {
        this.modalComponent.open();
    }
}