import { ChangeDetectionStrategy,Component } from '@angular/core';
import {   FormBuilder,FormGroup, FormsModule,ReactiveFormsModule, Validators } from '@angular/forms';
import { NgClass, NgFor, NgIf } from '@angular/common';
import {  RouterLink } from '@angular/router';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { DataserviceService } from '../Services/VisitorFormServices/dataservice.service';


@Component({
  selector: 'app-form-component',
  standalone: true,
  imports: [RouterLink,NgFor,NgIf,FormsModule,ReactiveFormsModule,NgClass,
     MatFormFieldModule,MatInputModule,],
  templateUrl: './form-component.component.html',
  styleUrl: './form-component.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})



export class FormComponentComponent {  
  addvisitorForm: FormGroup;  
  visitor :any[]=[]

  constructor(private apiService: DataserviceService,private fb: FormBuilder) 
  {
    this.addvisitorForm = this.fb.group({
      name: ['', [Validators.required]],     
      location:['', Validators.required],
      phoneNumber: ['', [Validators.required]],
      personInContact: ['',[ Validators.required]],
      purposeofvisit: ['', Validators.required],
      
    });
  }
ngOnInit(){
  this.apiService.getVisitorList().subscribe((response: any[]) => {
    this.visitor = response;
    console.log(this.visitor);
    
  });
}

onSubmit(): void {
  const formData = this.addvisitorForm.value;
  const visitorPayload = {
    name: formData.name,
    phoneNumber: formData.phoneNumber,
    personInContact: formData.personInContact,
    purposeOfVisit: formData.purposeofvisit,
    officeLocation: formData.location,   
  };

  console.log('Visitor Payload:', visitorPayload);

  this.apiService.createVisitor(visitorPayload).subscribe(
    (response) => {
      console.log('Visitor added successfully:', response);
     alert("Sucessfully added visitor")
    },
    (error) => {
      
      console.error('Error adding visitor and item:', error);
    }
  );
}
  }
