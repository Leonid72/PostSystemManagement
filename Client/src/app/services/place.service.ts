import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ErrorService } from './error.service';
import { catchError, Observable, throwError } from 'rxjs';
import { IPostItemDto } from '../interfaces/IPostItemDto';
import { IPlaceDto } from '../interfaces/IPlaceDto';
import { API_URL } from '../constants/constants';

@Injectable({
  providedIn: 'root'
})
export class PlaceService {


  constructor(private http : HttpClient,private errorService : ErrorService ) { }

  getAillPlaces() : Observable<IPlaceDto[]> {
      return this.http.get<IPlaceDto[]>(`${API_URL}/place`).pipe(
        catchError(this.errorHandler.bind(this))
      );
    }


     errorHandler(error: HttpErrorResponse) {
        this.errorService.handle(error.message)
        return throwError(()=>error.message)
      }
}
