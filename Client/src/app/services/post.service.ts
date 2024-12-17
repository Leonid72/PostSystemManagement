import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { IPostItemDto } from '../interfaces/IPostItemDto';
import { API_URL } from '../constants/constants';
import { ErrorService } from './error.service';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http : HttpClient,private errorService : ErrorService ) { }
  

  
  getAillPosts() : Observable<IPostItemDto[]> {
    return this.http.get<IPostItemDto[]>(`${API_URL}/postitem`).pipe(
      catchError(this.errorHandler.bind(this))
    );
  }

  deletePostItem(postItemId: number): Observable<void> {
    return this.http.delete<void>(`${API_URL}/postitem/${postItemId}`).pipe(
      catchError(this.errorHandler.bind(this))
    );
  }

  getPostItemById(postItemId: number): Observable<IPostItemDto> {
    return this.http.get<IPostItemDto>(`${API_URL}/postitem/${postItemId}`).pipe(
      catchError(this.errorHandler.bind(this))
    );
  }

  updatePostItem(postId: number, postData: Partial<IPostItemDto>): Observable<IPostItemDto> {
    return this.http.put<IPostItemDto>(`${API_URL}/postitem/${postId}`, postData).pipe(
      catchError(this.errorHandler.bind(this))
    );
  }

  
    addPostItem(formData: FormData): Observable<IPostItemDto> {
    return this.http.post<IPostItemDto>(`${API_URL}/postitem`, formData).pipe(
      catchError(this.errorHandler.bind(this))
    );
  }

  // addPostItem(post: IPostItemDto): Observable<IPostItemDto> {
  //   return this.http.post<IPostItemDto>(`${API_URL}/postitem`, post).pipe(
  //     catchError(this.errorHandler.bind(this))
  //   );
  // }

  errorHandler(error: HttpErrorResponse) {
    this.errorService.handle(error.message)
    return throwError(()=>error.message)
  }
}
