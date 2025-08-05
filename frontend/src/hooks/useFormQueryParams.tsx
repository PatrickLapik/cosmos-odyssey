import { useSearchParams } from "react-router";

export function useFormQueryParams<T extends Record<string, string>>() {
    const [params] = useSearchParams();

    const result = {} as T;

    for (const [key, value] of params.entries()) {
        if (key in result) {
            (result as any)[key] = value;
        };
    };

    return {
        result
    };
};

