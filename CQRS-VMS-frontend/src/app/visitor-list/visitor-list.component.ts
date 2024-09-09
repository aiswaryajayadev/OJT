import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { Visitor } from '../Models/visitor-interface';
import { DataserviceService } from '../Services/VisitorFormServices/dataservice.service';

@Component({
  selector: 'app-visitor-list',
  standalone: true,
  imports: [MatTableModule,CommonModule],
  templateUrl: './visitor-list.component.html',
  styleUrl: './visitor-list.component.scss'
})
export class VisitorListComponent {
  visitor = new MatTableDataSource<Visitor>();
  displayedColumns: string[] = ['position', 'name', 'purposeOfVisit', 'visitDate', 'location', 'hostName', 'phoneNumber'];

  constructor(private apiService: DataserviceService) {}

  ngOnInit() {

    this.apiService.startConnection();
    this.apiService.addVisitorCreatedListener((newVisitor: Visitor) => {
      this.visitor.data = [...this.visitor.data, newVisitor]; // Add the new visitor to the list
      console.log('New visitor added:', newVisitor);
    });
  
    this.apiService.getVisitorList().subscribe((response: Visitor[]) => {
      this.visitor.data = response;
      console.log(this.visitor);
    });
  }
}
