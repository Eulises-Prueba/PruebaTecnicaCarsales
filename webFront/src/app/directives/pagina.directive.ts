import {  Directive, OnChanges, OnInit, SimpleChanges, EventEmitter, ElementRef, Renderer2, HostListener } from "@angular/core";
import { input, output } from "@angular/core";

@Directive({
  selector: "[appPaginas]",
  exportAs: "appPaginas"
})
export class PaginaDirective implements OnChanges, OnInit {
  pageNumero = 1;
  totalPaginas = input<number>(1);
  pageChange = output<any>();

  constructor(private el: ElementRef, private renderer: Renderer2) {}

  ngOnInit() {
    this.setValue(this.pageNumero);
  }

  ngOnChanges({ pageNo, totalPages }: SimpleChanges) {
    if (totalPages) {
      this.onTotalPagesInput();
    }

    if (pageNo) {
      this.onPageNoInput();
    }
  }

  @HostListener("input", ["$event.target.value"]) onInput(val: string) {
    this.setValue(this.getParsedValue(val));
  }

  @HostListener("change", ["$event.target.value"]) onChange(val: string) {
    if (val === "") {
      this.setValue(1);
    }

    if (this.isOutOfRange(val)) {
      this.setValue(this.totalPaginas());
    }

    this.pageNumero = Number(this.el.nativeElement.value);
    this.pageChange.emit(this.pageNumero);
  }

  get isFirst(): boolean {
    return this.pageNumero === 1;
  }

  get isLast(): boolean {
    if(this.totalPaginas() <= 1) return false;
    return this.pageNumero === this.totalPaginas();
  }

  first() {
    this.setPage(1);
  }

  prev() {
    this.setPage(Math.max(1, this.pageNumero - 1));
  }

  next() {
    this.setPage(Math.min(this.totalPaginas(), this.pageNumero + 1));
  }

  last() {
    this.setPage(this.totalPaginas());
  }

  private setValue(val: string | number) {
    this.renderer.setProperty(this.el.nativeElement, "value", String(val));
  }

  private setPage(val: number) {
    this.pageNumero = val;
    this.setValue(this.pageNumero);
    this.pageChange.emit(this.pageNumero);
  }

  private getParsedValue(val: string): string {
    return val.replace(/(^0)|([^0-9]+$)/, "");
  }

  private isOutOfRange(val: string): boolean {
    return Number(val) > this.totalPaginas();
  }

  private onTotalPagesInput() {
    if (typeof this.totalPaginas() !== "number") {
      this.totalPaginas() == 1;
    }
  }

  private onPageNoInput() {
    if (
      typeof this.pageNumero !== "number" ||
      this.pageNumero < 1 ||
      this.pageNumero > this.totalPaginas()
    ) {
      this.pageNumero = 1;
    }

    this.setValue(this.pageNumero);
  }
}