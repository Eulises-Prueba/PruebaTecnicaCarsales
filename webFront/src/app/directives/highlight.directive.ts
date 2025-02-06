import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appHighlight]'
})
export class HighlightDirective {
  originalColor: string;

  constructor(private item: ElementRef) {
    this.originalColor = item.nativeElement.style.backgroundColor;
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.item.nativeElement.style.backgroundColor = this.originalColor;
  }

  @HostListener('mouseenter') onMouseEnter() {
    this.item.nativeElement.style.backgroundColor = 'rgba(85, 57, 228, 0.2)';
  }
}