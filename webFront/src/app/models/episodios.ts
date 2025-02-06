import { EpisodioData } from './episodioData';
import { EpisodioInfo } from './episodioInfo'

export interface Episodios {
    info: EpisodioInfo;
    results: Array<EpisodioData>;
}