<template>
    <div v-if="error">
        <h4>{{ error.title }}</h4>
        <div>
            <div v-for="(item, key) in error.messages" :key="key">
                {{ item }}
            </div>
        </div>
        <div>
            <input type="button" class="error-close" @click.prevent="close" value="Close" />
        </div>
    </div>
</template>

<script>
export default {
    name: "ErrorMessage",
    data() {
        return {
            error: null
        };
    },
    methods: {
        close() {
            this.$store.state.error = null;
        },
    },
    async mounted() {
        let res = this.$store.state.error;

        let error = await res.json();

        this.error = {
            title: error.exception,
            messages: error.messages,
        };

    }
};
</script>

<style>

</style>
