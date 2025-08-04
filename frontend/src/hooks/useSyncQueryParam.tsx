import { useSearchParams } from "react-router";

export const useSyncQueryParam = (key: string, defaultValue?: string) => {
  const [searchParams, setSearchParams] = useSearchParams();

  const value = searchParams.get(key) ?? defaultValue ?? "";

  const setValue = (newValue: string) => {
    const newParams = new URLSearchParams(searchParams);
    newParams.set(key, newValue);
    if (newValue.trim() === "") newParams.delete(key);
    setSearchParams(newParams);
  };

  return [value, setValue] as const;
}
