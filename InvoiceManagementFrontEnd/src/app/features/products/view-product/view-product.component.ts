import { Component, Input } from '@angular/core';
import { EditProductComponent } from '../create-or-edit-product/create-or-edit-product.component';
import { Product } from '../product.model';

@Component({
    selector: 'app-view-product',
    standalone: true,
    imports: [EditProductComponent],
    templateUrl: './view-product.component.html',
})
export class ViewProductComponent {
    @Input() product!: Product;
    @Input() title = 'Ver Producto';
}
