<template>
    <div>
        <input type="button" @click="onLoadData" value="Load data" />
        <input type="button" @click="onClearDB" value="Clear data" />
        <br>
        <input class="field" v-model="filter.yearFrom" type="number" placeholder="Year from"/>
        <input class="field" v-model="filter.yearTo" type="number" placeholder="Year to"/>
        <select class="field" v-model="filter.recclassID">
            <option :value="null">Select Class</option>
            <option v-for="(item, key) in getProperties.recclasses" :key="key" :value="item.id">{{ item.name }}</option>
        </select>
        <input class="field" v-model="filter.pattern" type="text" placeholder="Comet name"/>
        <input type="button" value="Search" @click="onLoadCometsByYear" />
    </div>
    <div v-if="getCometsReport != null">
        <table class="data-table">
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
                <tr v-if="getCometsReport.length == 0">
                    <td :colspan="table.header.length">Comets is not found!</td>
                </tr>
                <tr v-for="(data, key) in getCometsReport" :key="key">
                    <td v-for="(head) in table.header" :key="head.id">
                        {{ data[head.name] }}
                    </td>
                </tr>
            </tbody>
        </table>
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
                    { title: "Mass", name: "totalMass" }
                ],
                sortDirection: 1,
                sortBy: 'count'
            },
            filter: {
                yearFrom: "",
                yearTo: "",
                recclassID: null,
                pattern: ""
            }
        };
    },
    computed: {
        getProperties() {
            return this.$store.getters.properties
        },
        getCometsReport() {
            const direction = this.table.sortDirection
            const head = this.table.sortBy
            if (this.$store.getters.cometsReport) {

                return this.$store.getters.cometsReport.slice(0).sort(
                    direction === 1 ?
                        (a, b) => Number(b[head]) - Number(a[head]) :
                        (a, b) => Number(a[head]) - Number(b[head])
                );
            } else
                return null;
        }
    },
    created() {
        // fetch the data when the view is created and the data is
        // already being observed
        //this.fetchData();
    },
    watch: {
        // call again the method if the route changes
        //'$route': 'fetchData'
    },
    methods: {
        sort(head) {
            this.table.sortBy = head
            this.table.sortDirection *= -1
        },
        sortMethods(head, direction) {

            return direction === 1 ?
                (a, b) => Number(b[head]) - Number(a[head]) :
                (a, b) => Number(a[head]) - Number(b[head])

        },
        async onLoadData() {
            await this.$store.dispatch('onLoadComets')
            this.onLoadProperties();
        },
        onLoadProperties() {
            this.$store.dispatch('onGetProperties');
        },
        onLoadCometsByYear() {
            this.$store.dispatch('onGetCometsByYear', this.filter);
        },
        onClearDB() {
            this.$store.dispatch('onClearDB');
        },
    },
    mounted() {
        this.onLoadProperties();
    },
});
</script>

<style>

</style>