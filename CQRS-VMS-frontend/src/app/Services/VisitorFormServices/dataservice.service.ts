import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Visitor } from '../../Models/visitor-interface';
import * as signalR from '@microsoft/signalr';
import { HubConnection } from '@microsoft/signalr';


@Injectable({
  providedIn: 'root'
})

export class DataserviceService {
  private hubConnection: HubConnection | undefined ;
  constructor(private http:HttpClient) { } 


  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7112/visitorListHub')  // Use the correct API URL
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public addVisitorCreatedListener(visitorCreatedCallback: (visitor: Visitor) => void): void {
    if (this.hubConnection) {
      this.hubConnection.on('VisitorCreated', (visitor) => {
        visitorCreatedCallback(visitor);
      });
    } else {
      console.error('Hub connection is not initialized.');
    }
  }

   createVisitor(visitor:any):Observable<any[]>{
    console.log("log details",visitor);
    // visitor.OfficeLocationId = 1;
    const apiUrl="https://localhost:7112/api/visitors";
     return this.http.post<any>(apiUrl,visitor);
   }

   
   getVisitorList(): Observable<Visitor[]> {
    const apiUrl = "https://localhost:7112/api/visitors";
    return this.http.get<any[]>(apiUrl).pipe(
      map(response => response.map(visitor => ({
        name: visitor.name,
        purposeOfVisit: visitor.purposeOfVisit,
        visitDate: visitor.visitDate,
        officeLocation: visitor.officeLocation,
        hostName: visitor.hostName,
        phone: visitor.phone
      })))
    );
  }

   
   

};

