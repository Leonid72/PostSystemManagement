import { Component } from '@angular/core';
import { PostPageComponent } from "../post-page/post-page.component";
import { HeaderComponent } from "../../components/header/header.component";
import { FooterComponent } from "../../components/footer/footer.component";
import { GlobalErrorComponent } from "../../components/global-error/global-error.component";

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [PostPageComponent, HeaderComponent, FooterComponent, GlobalErrorComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {

}
