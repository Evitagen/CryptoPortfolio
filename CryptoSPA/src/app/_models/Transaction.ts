export interface Transaction {
    QtyModel: number;
    TransactionFee: number;
    dateTransaction: Date;
    PriceWhenBought: number;
    transactions?: Transaction[];
}
