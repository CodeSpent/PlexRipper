<template>
	<span v-if="pulse" class="status-indicator" :[status]="true" pulse />
	<span v-else class="status-indicator" :[status]="true" />
</template>

<script setup lang="ts">
// Stolen from: https://www.npmjs.com/package/vue-status-indicator
const props = defineProps<{ value: boolean }>();

const status = computed(() => {
	return props.value ? 'positive' : 'negative';
});

const pulse = computed(() => {
	return props.value;
});
</script>
