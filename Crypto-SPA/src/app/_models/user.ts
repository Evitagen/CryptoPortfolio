import { CoinsU } from './coinsU';

export interface User {
  coinHodles: any;
    id: number;
    username: string;
    price: number;
    coins?: CoinsU[];
}
