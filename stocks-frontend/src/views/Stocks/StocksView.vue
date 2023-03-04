<template>
    <h1>Stocks</h1>
    <SingleStock
        :stocksListDomain="this.stocksListDomain"
        :stockNameEndpoint="this.currentSelectedStock">
    </SingleStock>

    <Stocks
        :stocksListDomain="this.stocksListDomain"
        :stocksListEndpoint='this.stocksListEndpoint'
        @stockNameSelected="updateSelectedStock">
    </Stocks>

    <div class="footer">
        <p>Please note, if the value for a stock is <b>0 EUR</b> and time says <b>Last time checked: Never</b> it means a stock hasn't been selected for viewing or one selected doesn't exist anymore.</p>
    </div>
</template>

<script>
import Stocks from '../../components/Stocks/Stocks.vue'
import SingleStock from '../../components/Stocks/SingleStock.vue'

export default {
    name: 'StocksView',
    components: {
        Stocks, SingleStock
    },
    data() {
        return {
            stocksListDomain: 'http://localhost:8000',
            stocksListEndpoint: 'StocksList',
            currentSelectedStock: null
        }
    },
    methods: {
        updateSelectedStock(selectedStockName) {
            this.currentSelectedStock = selectedStockName            
        }
    }
}
</script>

<style scoped>
.footer {
   position: fixed;
   left: 0;
   bottom: 0;
   width: 100%;
   background-color: red;
   color: white;
   text-align: center;
}
</style>