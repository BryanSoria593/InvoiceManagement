import { Injectable } from '@angular/core';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';
const vfs = (pdfFonts as any).pdfMake?.vfs || (pdfFonts as any).vfs;
if (vfs && typeof vfs === 'object') {
    (pdfMake as any).vfs = vfs;
}

@Injectable({ providedIn: 'root' })
export class InvoicePdfService {

    generateInvoicePdf(invoice: any) {
        const docDefinition = {
            content: [
                {
                    columns: [
                        [
                            { text: invoice.companyName || '', style: 'header' },
                            { text: invoice.companyAddress || '', style: 'subheader' },
                            { text: `Teléfono: ${invoice.companyPhone || ''}`, style: 'small' },
                            { text: `Email: ${invoice.companyEmail || ''}`, style: 'small' }
                        ],
                        { text: `FACTURA Nº ${invoice.number || ''}`, alignment: 'right', style: 'header' }
                    ]
                },
                { text: '\n' },
                {
                    table: {
                        widths: ['*'],
                        body: [
                            [
                                { text: 'FACTURAR A', style: 'tableHeader', alignment: 'left' }
                            ],
                            [
                                {
                                    text: `${invoice.customerName || ''}`,
                                    style: 'small', bold: true
                                }
                            ],
                            [
                                {
                                    text: `${invoice.customerAddress || ''}`,
                                    style: 'small'
                                }
                            ],
                            [
                                {
                                    text: `Teléfono: ${invoice.customerPhone || ''}`,
                                    style: 'small'
                                }
                            ],
                            [
                                {
                                    text: `Email: ${invoice.customerEmail || ''}`,
                                    style: 'small'
                                }
                            ]
                        ]
                    },
                    layout: 'noBorders'
                },
                { text: '\n' },
                {
                    table: {
                        widths: ['*', '*', '*'],
                        body: [
                            [
                                { text: 'VENDEDOR', style: 'tableHeaderBox', alignment: 'left', margin: [0, 4, 0, 4] },
                                { text: 'FECHA', style: 'tableHeaderBox', alignment: 'left', margin: [0, 4, 0, 4] },
                                { text: 'FORMA DE PAGO', style: 'tableHeaderBox', alignment: 'left', margin: [0, 4, 0, 4] }
                            ],
                            [
                                { text: `${invoice.sellerName || ''}`, style: 'small', alignment: 'left', margin: [0, 4, 0, 4] },
                                { text: `${invoice.date || ''}`, style: 'small', alignment: 'left', margin: [0, 4, 0, 4] },
                                { text: `${invoice.paymentMethodName || ''}`, style: 'small', alignment: 'left', margin: [0, 4, 0, 4] }
                            ]
                        ]
                    },
                    layout: {
                        fillColor: function (rowIndex: number) { return rowIndex === 0 ? '#2c3e50' : null; },
                        defaultBorder: false
                    }
                },
                { text: '\n' },
                {
                    table: {
                        headerRows: 1,
                        widths: [40, '*', 70, 70],
                        body: [
                            [
                                { text: 'CANT.', style: 'tableHeader' },
                                { text: 'DESCRIPCION', style: 'tableHeader' },
                                { text: 'PRECIO UNIT.', style: 'tableHeader' },
                                { text: 'PRECIO TOTAL', style: 'tableHeader' }
                            ],
                            ...invoice.details.map((d: any) => [
                                d.quantity,
                                d.productName,
                                { text: d.unitPrice.toFixed(2), alignment: 'right' },
                                { text: d.total.toFixed(2), alignment: 'right' }
                            ]),
                            [
                                { text: '', colSpan: 2 }, {}, { text: 'SUBTOTAL $', alignment: 'right' },
                                { text: invoice.subtotal.toFixed(2), alignment: 'right' }
                            ],
                            [
                                { text: '', colSpan: 2 }, {}, { text: `IVA (${invoice.vatPercentage}%) $`, alignment: 'right' },
                                { text: invoice.iva.toFixed(2), alignment: 'right' }
                            ],
                            [
                                { text: '', colSpan: 2 }, {}, { text: 'TOTAL $', alignment: 'right' },
                                { text: invoice.total.toFixed(2), alignment: 'right' }
                            ]
                        ]
                    }
                },
                { text: '\n\nGracias por su compra!', alignment: 'center', bold: true }
            ],
            styles: {
                header: { fontSize: 16, bold: true },
                subheader: { fontSize: 12, bold: true },
                small: { fontSize: 9 },
                tableHeader: { fillColor: '#2c3e50', color: 'white', bold: true, fontSize: 10 },
                tableHeaderBox: { fillColor: '#2c3e50', color: 'white', bold: true, fontSize: 10, margin: [0, 2, 0, 2] }
            }
        };
        (pdfMake as any).createPdf(docDefinition).open();
    }
}
