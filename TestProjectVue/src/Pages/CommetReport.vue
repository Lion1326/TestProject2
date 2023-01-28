<template>
    <div>
        <div>
            <input type="button" @click="onLoadData" value="Load data" />
            <input type="button" @click="onClearDB" value="Clear data" />
        </div>
        <div>
            <select class="field" v-model="filter.yearFrom">
                <option :value="null">Year from</option>
                <option v-for="(item, key) in getProperties.years" :key="key" :value="item">{{ item }}
                </option>
            </select>
            <select class="field" v-model="filter.yearTo">
                <option :value="null">Year to</option>
                <option v-for="(item, key) in getProperties.years" :key="key" :value="item">{{ item }}
                </option>
            </select>
            <select class="field" v-model="filter.recclassID">
                <option :value="null">Select Class</option>
                <option v-for="(item, key) in getProperties.recclasses" :key="key" :value="item.id">{{ item.name }}
                </option>
            </select>
            <input class="field" v-model="filter.pattern" type="text" placeholder="Comet name" />
            <input type="button" value="Search" @click="onLoadCometsByYear" />
        </div>
    </div>
    <div v-if="getCometsReport != null">
        <table class="data-table" v-if="getCometsReport.length > 0">
            <thead>
                <tr class="header">
                    <td v-for="(head, key) in table.header" :key="key" @click="sort(head.name)">
                        {{ head.title }}
                        <span v-if="head.name == table.sortBy">
                            <span v-if="table.sortDirection == 1"> &#9660;</span>
                            <span v-else> &#9650;</span>
                        </span>
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(data, key) in getCometsReport" :key="key">
                    <td v-for="(head) in table.header" :key="head.id">
                        {{ data[head.name] }}
                    </td>
                </tr>
            </tbody>
            <tfoot class="footer">
                <tr>
                    <td>Total</td>
                    <td>{{ getFooterTable.totalCount }}</td>
                    <td>{{ getFooterTable.totalMass }}</td>
                </tr>
            </tfoot>
        </table>
        <div v-else class="error-table">
            <b>Comets is not found!</b>
        </div>
    </div>
</template>

<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    name: 'CommetReport',
    data() {
        return {
            table: {
                header: [
                    { title: "Year", name: "year" },
                    { title: "Count", name: "count" },
                    { title: "Mass", name: "mass" }
                ],
                sortDirection: 1,
                sortBy: 'count'
            },
            filter: {
                yearFrom: null,
                yearTo: null,
                recclassID: null,
                pattern: ""
            }
        };
    },
    computed: {
        getProperties() {
            return this.$store.getters.properties;
        },
        getCometsReport() {
            const direction = this.table.sortDirection;
            const head = this.table.sortBy;
            if (this.$store.getters.cometsReport) {
                return this.$store.getters.cometsReport.list.slice(0).sort(
                    direction === 1 ?
                        (a, b) => Number(b[head]) - Number(a[head]) :
                        (a, b) => Number(a[head]) - Number(b[head])
                );
            } else
                return null;
        },
        getFooterTable() {
            if (this.$store.getters.cometsReport) {
                return this.$store.getters.cometsReport;
            }
            return null;
        }
    },
    created() {
    },
    watch: {
        // 'filter.yearFrom'(newValue){
        //     if(newValue==='' || newValue ==null){
        //         alert("yearFrom is empty");
        //     }
        // },
        // 'filter.yearTo'(newValue){
        //     if(newValue===''){
        //         alert("yearTo is empty");
        //     }
        // }
    },
    methods: {
        sort(head) {
            this.table.sortBy = head;
            this.table.sortDirection *= -1;
        },
        sortMethods(head, direction) {
            return direction === 1 ?
                (a, b) => Number(b[head]) - Number(a[head]) :
                (a, b) => Number(a[head]) - Number(b[head])
        },
        async onLoadData() {
            await this.$store.dispatch('onLoadComets');
            this.onLoadProperties();
        },
        async onLoadProperties() {
            await this.$store.dispatch('onGetProperties');
        },
        // validateFilter() {
        //     return this.filter.yearFrom && this.filter.yearTo;
        // },
        async onLoadCometsByYear() {
            //if (this.validateFilter())
            await this.$store.dispatch('onGetCometsByYear', this.filter);
        },
        async onClearDB() {
            await this.$store.dispatch('onClearDB');
        },
    },
    async mounted() {
        await this.onLoadProperties();
    },
});
</script>

<style>

</style>