<template>
      <div v-if="this.stocks.length">
        <div 
            v-for="stock in stocks" 
            :key="stock.id" 
            class="stocks"
            @click="selectStock(stock)">
            <h3 :class="{ act: stock.active }">{{ stock.stockName }}</h3>
        </div>
    </div>

    <div v-else>
        <p>Loading stocks...</p>
    </div>
</template>

<script>
export default {
    name: 'Stocks',
    props: ['stocksListDomain', 'stocksListEndpoint'],
    data() {
        return {
            stocks: [],
            currentSelectedStock: null
        }
    },
    mounted() {
        let stocksListLink = this.stocksListDomain + '/' + this.stocksListEndpoint
        fetch(stocksListLink)
            .then((res) => res.json())            
            .then((data) => this.stocks = data)
            .then(() => this.stockDataEnrichment())
            .catch((err) => console.log(err))
    },
    methods: {
        // Dirty trick to add a state of the selected delivery
        stockDataEnrichment() {
                for(var i=0; i<this.stocks.length;i++) {
                    this.stocks[i]['active'] = false
                }
        },
        deactivateAllStocks() {
            for (var i=0; i<this.stocks.length;i++) {
                this.stocks[i]['active'] = false
            }
        },
        selectStock(stock) {
            this.deactivateAllStocks()
            stock.active = true
            this.currentSelectedStock = stock.stockName

            // Inform the parent of the selected stock
            this.$emit('stockNameSelected', this.currentSelectedStock)
        }
    }
}
</script>

<style scoped>
.stocks {
    cursor: pointer;
}

h3  {
    background: #f4f4f4;
    padding: 20px;
    border-radius: 10px;
    margin: 10px auto;
    max-width: 600px;
    color: #444;
}

h3.act {
    background: #f4f4f4;
    padding: 20px;
    border-radius: 10px;
    margin: 10px auto;
    max-width: 600px;
    color: #444;
    background-color: #87ffc8;
}
</style>