import { useCallback, useRef } from "react";
import { useSearchParams } from "react-router";

export const useSyncQueryParam = (
  key: string,
  debounceMs: number = 300,
  defaultValue?: string
) => {
  const [searchParams, setSearchParams] = useSearchParams();
  const timeoutRef = useRef<number | null>(null);

  const value = searchParams.get(key) ?? defaultValue ?? "";

  const setValue = useCallback(
    (newValue: string) => {
      if (timeoutRef.current) {
        clearTimeout(timeoutRef.current);
      }

      timeoutRef.current = window.setTimeout(() => {
        const newParams = new URLSearchParams(searchParams);
        if (newValue.trim() === "") {
          newParams.delete(key);
        } else {
          newParams.set(key, newValue);
        }
        setSearchParams(newParams);
      }, debounceMs);
    },
    [key, searchParams, setSearchParams, debounceMs]
  );

  return [value, setValue] as const;
};
