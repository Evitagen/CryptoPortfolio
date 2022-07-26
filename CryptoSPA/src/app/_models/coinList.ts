export interface CoinList {
    length: number;
    name: string;
    price: number;
    volume: number;
    circulating: number;
    marketcap: number;
    totalSupply: number;
    percentChange1hr: number;
    percentChange24hr: number;
    percentChange7day: number;
    coinMcapRank: number;
    coinID: string;
    lastUpdated: Date;
    imagelocation: string;
    prevPriceHigherLowerSame: number;  // 0 = Same, 1 = Lower, 2 = Higher
}
