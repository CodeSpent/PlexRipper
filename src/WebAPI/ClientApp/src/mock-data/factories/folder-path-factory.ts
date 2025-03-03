import { randDirectoryPath, randProductName } from '@ngneat/falso';
import { FolderPathDTO, FolderType, PlexMediaType } from '@dto/mainApi';
import { checkConfig, MockConfig } from '~/mock-data';
import Convert from '@class/Convert';

let folderPathIdIndex = 1;

export function generateFolderPath({
	id,
	type,
	config = {},
	partialData = {},
}: {
	id: number;
	type: PlexMediaType;
	partialData?: Partial<FolderPathDTO>;
	config?: Partial<MockConfig>;
}): FolderPathDTO {
	checkConfig(config);

	return {
		id,
		directory: randDirectoryPath(),
		displayName: randProductName(),
		folderType: Convert.mediaTypeToFolderType(type),
		isValid: true,
		mediaType: type,
		...partialData,
	};
}

export function generateFolderPaths({
	type,
	config = {},
	partialData = {},
}: {
	type: PlexMediaType;
	partialData?: Partial<FolderPathDTO>;
	config?: Partial<MockConfig>;
}): FolderPathDTO[] {
	const validConfig = checkConfig(config);
	return Array(validConfig.folderPathCount)
		.fill(null)
		.map(() => generateFolderPath({ id: folderPathIdIndex++, type, partialData, config }));
}

export function generateDefaultFolderPaths({ config = {} }: { config?: Partial<MockConfig> }): FolderPathDTO[] {
	checkConfig(config);

	const defaultFolderPaths: FolderPathDTO[] = [];

	const mediaTypes: PlexMediaType[] = [
		PlexMediaType.None,
		PlexMediaType.Movie,
		PlexMediaType.TvShow,
		PlexMediaType.Music,
		PlexMediaType.Photos,
		PlexMediaType.OtherVideos,
		PlexMediaType.Games,
		PlexMediaType.None,
		PlexMediaType.None,
		PlexMediaType.None,
	];

	const folderTypes: FolderType[] = [
		FolderType.DownloadFolder,
		FolderType.MovieFolder,
		FolderType.TvShowFolder,
		FolderType.MusicFolder,
		FolderType.PhotosFolder,
		FolderType.OtherVideosFolder,
		FolderType.GamesVideosFolder,
		FolderType.None,
		FolderType.None,
		FolderType.None,
	];

	for (let i = 0; i < 10; i++) {
		defaultFolderPaths.push(
			generateFolderPath({
				id: i + 1,
				type: mediaTypes[i],
				partialData: {
					folderType: folderTypes[i],
				},
				config,
			}),
		);
	}
	return defaultFolderPaths;
}
