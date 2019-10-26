import { CoinsHodle } from './coinsHodle';

export interface Portfolio {
    portfolioID: number;
    portfolioName: string;
    userid: number;
    coinsHodle?: CoinsHodle[];
}
