import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ThemeService } from '../../../ui/src/lib/services/theme.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  themeService = inject(ThemeService);

  selectedTheme = '';

  ngOnInit(): void {
    const theme = this.themeService.getSelectedTheme();
    this.selectedTheme = theme.name;
  }

  changeTheme(themeName: string) {
    this.themeService.setSelectedTheme(themeName);
    this.selectedTheme = themeName;
  }
}
